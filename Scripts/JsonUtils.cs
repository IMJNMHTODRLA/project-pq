using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ProjectPQ.Scripts;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class JsonTypeIdAttribute(ulong id) : Attribute
{
    public ulong Id { get; } = id;
}

public sealed class JsonTypeIdBinder : ISerializationBinder
{
    private readonly Dictionary<ulong, Type> _idToType = [];
    private readonly Dictionary<Type, ulong> _typeToId = [];

    public JsonTypeIdBinder()
    {
        foreach (
            Type type in AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(GetLoadableTypes)
        )
        {
            JsonTypeIdAttribute? typeId =
                type.GetCustomAttribute<JsonTypeIdAttribute>();
                    
#if DEBUG
            bool hasJsonProperty =
                type.GetProperties(
                    BindingFlags.Instance | BindingFlags.Static |
                    BindingFlags.Public | BindingFlags.NonPublic
                ).Any(property =>
                    property.GetCustomAttribute<JsonPropertyAttribute>() is not null
                ) || type.GetFields(
                    BindingFlags.Instance | BindingFlags.Static |
                    BindingFlags.Public | BindingFlags.NonPublic
                ).Any(field =>
                    field.GetCustomAttribute<JsonPropertyAttribute>() is not null
                );

            if (hasJsonProperty && typeId is null)
                throw new InvalidOperationException(
                    $"[JsonTypeId]가 지정되지 않은 타입에 " +
                    $"[JsonProperty]가 사용되었습니다: {type.FullName}"
                );
#endif

            if (typeId is null)
                continue;

            ulong id = typeId.Id;

            if (_idToType.TryGetValue(id, out Type? existingType))
                throw new InvalidOperationException(
                    $"[JsonTypeId] 중복된 ID입니다. " +
                    $"ID: 0x{id:X16}, " +
                    $"Type1: {existingType.FullName}, " +
                    $"Type2: {type.FullName}"
                );

            _idToType.Add(id, type);
            _typeToId.Add(type, id);
        }
    }

    public void BindToName(
        Type serializedType,
        out string? assemblyName,
        out string? typeName
    )
    {
        assemblyName = null;

        if (!_typeToId.TryGetValue(serializedType, out ulong id))
            throw new InvalidOperationException(
                $"[JsonTypeId] ID가 지정되지 않은 타입입니다: " +
                $"{serializedType.FullName}"
            );

        typeName = id.ToString("X16");
    }

    public Type BindToType(string? assemblyName, string typeName)
    {
        if (!ulong.TryParse(
            typeName,
            NumberStyles.HexNumber,
            CultureInfo.InvariantCulture,
            out var id
        ))
            throw new InvalidOperationException(
                $"[JsonTypeId] 잘못된 ID입니다: {typeName}"
            );

        if (!_idToType.TryGetValue(id, out var type))
            throw new InvalidOperationException(
                $"[JsonTypeId] 알 수 없는 ID입니다: 0x{id:X16}"
            );

        return type;
    }

    private static IEnumerable<Type> GetLoadableTypes(Assembly assembly)
    {
        try
        {
            return assembly.GetTypes();
        }
        catch (ReflectionTypeLoadException exception)
        {
            return exception.Types.OfType<Type>();
        }
    }
}

public sealed class ExplicitPropertiesContractResolver : DefaultContractResolver
{
    public ExplicitPropertiesContractResolver()
    {
        NamingStrategy = new SnakeCaseNamingStrategy()
        {
            OverrideSpecifiedNames = false,
            ProcessDictionaryKeys = true
        };
    }

    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        JsonProperty property = base.CreateProperty(member, memberSerialization);

        bool hasJsonPropertyAttribute = member.GetCustomAttribute<JsonPropertyAttribute>() != null;
        if (!hasJsonPropertyAttribute)
            property.Ignored = true;

        return property;
    }
}

public static class JsonUtils
{
    private readonly static JsonSerializerSettings settings = new()
    {
        TypeNameHandling = TypeNameHandling.Auto,
        SerializationBinder = new JsonTypeIdBinder(),
        
        NullValueHandling = NullValueHandling.Include,
        DefaultValueHandling = DefaultValueHandling.Include,
        Formatting = Formatting.None,
        
        ContractResolver = new ExplicitPropertiesContractResolver(),

        ConstructorHandling = ConstructorHandling.Default,
        MissingMemberHandling = MissingMemberHandling.Ignore,
        ObjectCreationHandling = ObjectCreationHandling.Auto,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        PreserveReferencesHandling = PreserveReferencesHandling.Objects,

        DateFormatString = @"yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK",
        DateTimeZoneHandling = DateTimeZoneHandling.Utc,
    };

    public static string Serialize(object? obj) =>
        JsonConvert.SerializeObject(obj, settings);
    
    public static T? Deserialize<T>(string? json)
    {
        if (json == null) return default;
        return JsonConvert.DeserializeObject<T>(json, settings);
    }
}