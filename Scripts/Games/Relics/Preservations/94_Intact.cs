namespace ProjectPQ.Scripts.Games.Relics.Preservations;

[JsonTypeId(0x296E5C0A18DF3927)]
public class Intact : RelicPreservation
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "온전한";

    public override float Percent => 0.10f;
    public override double Rate => 1.0;
}