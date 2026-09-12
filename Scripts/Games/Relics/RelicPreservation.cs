using ProjectPQ.Scripts.Games.Relics.Preservations;

namespace ProjectPQ.Scripts.Games.Relics;

//보존도
public abstract partial class RelicPreservation
{
    protected abstract string Color { get; }
    protected abstract string BaseName { get; }

    public string Name => $"[color={ColorUtils.White}][[/color][color={Color}]{BaseName}[/color][color={ColorUtils.White}]][/color]";

    public abstract double Rate { get; }
    public abstract float Percent { get; }
}

public abstract partial class RelicPreservation
{
    public static readonly Intact Intact = new();
    public static readonly Preserved Preserved = new();
    public static readonly WornOut WornOut = new();
    public static readonly Damaged Damaged = new();
    public static readonly Destroyed Destroyed = new();
    public static readonly Piece Piece = new();

    private static readonly RelicPreservation[] _allPreservations =
    [
        Intact,
        Preserved,
        WornOut,
        Damaged,
        Destroyed,
        Piece
    ];

    public static RelicPreservation GetRandPreservation()
    {
        foreach (RelicPreservation pre in _allPreservations)
            if (pre.Percent.Chance())
                return pre;

        return Piece;
    }
}
