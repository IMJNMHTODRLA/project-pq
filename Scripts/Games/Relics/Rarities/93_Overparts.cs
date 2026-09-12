namespace ProjectPQ.Scripts.Games.Relics.Rarities;

[JsonTypeId(0x1B56BD9A69D5E7E1)]
public sealed class Overparts : RelicRarity
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "오버파츠";

    public override float Percent => 0.0001f;
    public override long DailyGold => 3_800L;
}