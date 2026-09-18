using System;
using System.Collections.Generic;
using Godot;
using ProjectPQ.Scripts.Games.Props.DigPoints;
using ProjectPQ.Scripts.Games.Relics;
using ProjectPQ.Scripts.Games.Relics.Relics;

namespace ProjectPQ.Scripts.Games.Maps;

public abstract partial class ExcavationMap : Map
{
    public abstract override ExcavationMapData LinkMapData { get; }

    /// <summary>
    /// 유물 풀
    /// </summary>
    protected virtual IReadOnlyDictionary<RelicRarity, IReadOnlyList<Func<Relic>>>
        RelicPool => new Dictionary<RelicRarity, IReadOnlyList<Func<Relic>>>();

    public virtual Relic? GetRandRelic()
    {
        if (RelicPool.Count == 0) return null;

        RelicRarity? randRarity = RelicRarity.GetRandRarity([..RelicPool.Keys]);
        if (randRarity == null) return null;

        if (!RelicPool.TryGetValue(randRarity, out IReadOnlyList<Func<Relic>>? factoryList))
            return null;

        return factoryList.NextElement().Invoke();
    }

    protected override void ActivateMap()
    {
        base.ActivateMap();

        foreach (Vector2 coord in LinkMapData.DigPointCoords)
        {
            DigPoint digPoint = SceneLoader.Load<DigPoint, DigPointArgs>(new(this));
            digPoint.OnExcavated += OnExcavated;
            digPoint.GlobalPosition = coord;
        }
    }

    private void OnExcavated(DigPoint dig)
    {
        Vector2 pos = dig.GlobalPosition;
        LinkMapData.RemoveDigPoints(pos);
    }
}