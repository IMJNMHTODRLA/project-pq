using System;
using System.Collections.Generic;
using ProjectPQ.Scripts.Games.Relics;
using ProjectPQ.Scripts.Games.Relics.Relics;

namespace ProjectPQ.Scripts.Games.Maps;

public abstract partial class ExcavationMap : Map
{
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
}