namespace ProjectPQ.Scripts.Games.Relics.Rarities;

[JsonTypeId(0xF0C299E57FD3AFD8)]
public sealed class Legendary : RelicRarity
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "전설적인";

    public override float Percent => 0.03f;
    public override long DailyGold => 1_100L;
}