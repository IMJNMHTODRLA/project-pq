namespace ProjectPQ.Scripts.Games.Relics.Preservations;

[JsonTypeId(0x65D00DAA582E6BC6)]
public class Damaged : RelicPreservation
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "손상된";

    public override float Percent => 0.40f;
    public override double Rate => 0.40;
}