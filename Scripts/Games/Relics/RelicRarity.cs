using ProjectPQ.Scripts.Games.Relics.Rarities;

namespace ProjectPQ.Scripts.Games.Relics;

//희귀도
public abstract partial class RelicRarity
{
    protected abstract string Color { get; }
    protected abstract string BaseName { get; }

    public string Name => $"[color={ColorUtils.White}][[/color][color={Color}]{BaseName}[/color][color={ColorUtils.White}]][/color]";

    public abstract float Percent { get; }
    public abstract long DailyGold { get; }
}

public abstract partial class RelicRarity
{
    public static readonly Overparts Overparts = new();
    public static readonly Myth Myth = new();
    public static readonly Legendary Legendary = new();
    public static readonly Historic Historic = new();
    public static readonly Rare Rare = new();
    public static readonly Precious Precious = new();
    public static readonly Common Common = new();

    private static readonly RelicRarity[] _allRarities =
    [
        Overparts,
        Myth,
        Legendary,
        Historic,
        Rare,
        Precious,
        Common
    ];
    
    public static RelicRarity? GetRandRarity(params RelicRarity[] allowedRarities)
    {
        RelicRarity[] pool = allowedRarities.Length > 0 ? allowedRarities : _allRarities;

        foreach (RelicRarity rarity in pool)
            if (rarity.Percent.Chance())
                return rarity;

        return null;
    }
}
