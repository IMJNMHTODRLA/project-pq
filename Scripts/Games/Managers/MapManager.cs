using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Maps;

namespace ProjectPQ.Scripts.Games.Managers;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RegisterMapDataAttribute : Attribute;

[JsonTypeId(0x0259_5261_EA3A_E960)]
public sealed class MapManager : GameService, IServiceDirect<MapManager>
{
    public static MapManager Self => GameManager.Self.Map;

    [JsonProperty]
    public MapData[] Maps
    {
        get => [.._maps.Values];
        init
        {
            foreach (MapData map in value)
                _maps[map.Type] = map;
        }
    }
    private readonly Dictionary<Type, MapData> _maps = [];

    public Map? CurrentMap
    {
        get; set
        {
            field?.SafeQueueFree();
            field = value;
        }
    } = null;

    public T? Get<T>()
        where T : MapData
    {
        _maps.TryGetValue(typeof(T), out MapData? data);
        return (T?) data;
    }

    public T GetOrThrow<T>()
        where T : MapData
    => Get<T>() ??
        throw new InvalidOperationException(
            $"{nameof(T)} is not registered in {nameof(MapManager)}."
        );

    public override void OnLoad()
    {
        _maps.Clear();
        Assembly assembly = Assembly.GetExecutingAssembly();

        IEnumerable<Type> types = assembly
            .GetTypes()
            .Where(type =>
                type.IsClass &&
                !type.IsAbstract &&
                typeof(MapData).IsAssignableFrom(type) &&
                type.GetCustomAttribute<RegisterMapDataAttribute>() != null
            );

        foreach (Type type in types)
        {
            MapData data = (MapData) Activator.CreateInstance(type)!;
            _maps.Add(type, data);
        }
    }
}