namespace ProjectPQ.Scripts.Games.Relics.Rarities;

[JsonTypeId(0x93814013DEFE1FF5)]
public sealed class Common : RelicRarity
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "흔한";

    public override float Percent => 0.50f;
    public override long DailyGold => 100L;
}