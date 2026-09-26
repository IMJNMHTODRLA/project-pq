using ProjectPQ.Scripts.Games.Items.Relics;
using System;
using ProjectPQ.Scripts.Games.Items;

namespace ProjectPQ.Scripts.Games.Upgrades.Brush;

public class BrushUpgrade : ItemCostUpgrade<double>
{
    protected override string BaseName => "붓";
    protected override string BaseDescription => "설명 추가하기";
    protected override string BaseEffect => "추후에 추가";

    protected override long BaseCost => 500;
    protected override (Func<Item, bool>, int)[] BaseItemCost { get; } =
    [
        (
            item => item is Relic relic && relic.IsPiece,
            3
        )
    ];
    protected override float BaseCostMultiple => 1.5f;

    public override int MaxLevel => int.MaxValue;

    public override double EffectValue => 0.7 * Level;
}