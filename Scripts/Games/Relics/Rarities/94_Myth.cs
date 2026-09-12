namespace ProjectPQ.Scripts.Games.Relics.Rarities;

[JsonTypeId(0xEB4DA5773A5A2FA3)]
public sealed class Myth : RelicRarity
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "신화";

    public override float Percent => 0.001f;
    public override long DailyGold => 2_000L;
}