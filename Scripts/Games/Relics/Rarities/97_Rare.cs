namespace ProjectPQ.Scripts.Games.Relics.Rarities;

[JsonTypeId(0xD4195EFA36FBDBB3)]
public sealed class Rare : RelicRarity
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "희귀한";

    public override float Percent => 0.16f;
    public override long DailyGold => 350L;
}