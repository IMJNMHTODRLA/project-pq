namespace ProjectPQ.Scripts.Games.Relics.Preservations;

[JsonTypeId(0xE33B7AE680FD1479)]
public class Piece : RelicPreservation
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "조각";

    public override float Percent => 1.0f;
    public override double Rate => 0.0;
}