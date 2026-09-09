namespace ProjectPQ.Scripts.Games.Items.Relics.Rarities;

public record class Legendary : RelicRarity
{
    protected override string BaseName => "전설적인";

    public override long DailyGoldLimit => 1_100L;
}