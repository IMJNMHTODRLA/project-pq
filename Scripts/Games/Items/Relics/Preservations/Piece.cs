namespace ProjectPQ.Scripts.Games.Items.Relics.Preservations;

public record class Piece : RelicPreservation
{
    protected override string BaseName => "조각";

    public override double PreservationRate { get; } = 0.0;
}