namespace ProjectPQ.Scripts.Games.Items.Relics.Preservations;

public record class Intact : RelicPreservation
{
    protected override string BaseName => "온전한";

    public override double PreservationRate { get; } = 1.0;
}