namespace ProjectPQ.Scripts.Games.Relics.Rarities;

[JsonTypeId(0xD530654A77F9EA7D)]
public sealed class Historic : RelicRarity
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => "유서 깊은";

    public override float Percent => 0.09f;
    public override long DailyGold => 600L;
}