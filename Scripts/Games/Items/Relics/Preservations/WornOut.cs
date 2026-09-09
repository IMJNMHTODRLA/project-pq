namespace ProjectPQ.Scripts.Games.Items.Relics.Preservations;

public record class WornOut : RelicPreservation
{
    protected override string BaseName => "마모된";

    public override double PreservationRate { get; } = 0.6;
}