namespace ProjectPQ.Scripts.Games.Items.Relics.Preservations;

public record class Damaged : RelicPreservation
{
    protected override string BaseName { get; } = "손상된";

    public override double PreservationRate { get; } = 0.4;
}