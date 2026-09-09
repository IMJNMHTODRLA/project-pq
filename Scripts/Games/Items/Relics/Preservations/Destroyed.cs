namespace ProjectPQ.Scripts.Games.Items.Relics.Preservations;

public record class Destroyed : RelicPreservation
{
    protected override string BaseName => "파괴된";

    public override double PreservationRate { get; } = 0.20;
}