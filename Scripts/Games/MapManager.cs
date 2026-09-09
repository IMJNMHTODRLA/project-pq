using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Maps;
using ProjectPQ.Scripts.Games.Maps.Museums;

namespace ProjectPQ.Scripts.Games;

[method: JsonConstructor]
[JsonTypeId(0x0259_5261_EA3A_E960)]
public sealed class MapManagerSaveData(MapData[]? maps = null)
{
    [JsonProperty]
    public MapData[] Maps { get; } = maps ?? [];
}

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class RegisterMapDataAttribute : Attribute;

public sealed partial class MapManager : Singleton<MapManager>
{
    public void Reset()
    {
        InitializeMap();
        CurrentMap = null;
    }

    public bool GameStart(MapManagerSaveData? saveData = null)
    {
        Reset();

        if (saveData != null)
        {
            foreach (MapData map in saveData.Maps)
                _maps[map.Type] = map;
        }

        SceneLoader.Change<Museum>();
        return true;
    }

    public Map? CurrentMap
    {
        get => field;
        set
        {
            field?.QueueFree();
            field = value;
        }
    } = null;
    private readonly Dictionary<Type, MapData> _maps = [];

    public T? Get<T>()
        where T : MapData
    {
        _maps.TryGetValue(typeof(T), out MapData? data);
        return (T?) data;
    }

    private void InitializeMap()
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

    protected override void OnAutoload() => Reset();
    protected override void OnTick(double delta) {}
}
