namespace ProjectPQ.Scripts.Games.Relics.Preservations;

[JsonTypeId(0x680DB1163BBD4A09)]
public class Preserved : RelicPreservation
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "보존된";

    public override float Percent => 0.20f;
    public override double Rate => 0.80;
}