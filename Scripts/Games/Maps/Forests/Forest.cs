using System;
using System.Collections.Generic;
using ProjectPQ.Scripts.Games.Relics;
using ProjectPQ.Scripts.Games.Relics.Relics;
using ProjectPQ.Scripts.Games.Relics.Relics.Forests.Commons;

namespace ProjectPQ.Scripts.Games.Maps.Forests;

public partial class Forest : ExcavationMap, IScene<EmptyArgs>
{
    public static string ScenePath => "res://Scenes/Games/Maps/Forests/Forest.tscn";

    protected override IReadOnlyDictionary<RelicRarity, IReadOnlyList<Func<Relic>>> RelicPool { get; } =
        new Dictionary<RelicRarity, IReadOnlyList<Func<Relic>>>()
        {
            [RelicRarity.Common] = [
                () => new OldPotteryFragments(),
            ],
        };
}