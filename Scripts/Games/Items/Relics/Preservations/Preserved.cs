namespace ProjectPQ.Scripts.Games.Items.Relics.Preservations;

public record class Preserved : RelicPreservation
{
    protected override string BaseName => "보존된";

    public override double PreservationRate { get; } = 0.8;
}