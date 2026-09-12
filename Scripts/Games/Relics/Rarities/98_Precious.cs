namespace ProjectPQ.Scripts.Games.Relics.Rarities;

[JsonTypeId(0x3D4C3BB82D6BE890)]
public sealed class Precious : RelicRarity
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "귀중한";

    public override float Percent => 0.35f;
    public override long DailyGold => 180L;
}