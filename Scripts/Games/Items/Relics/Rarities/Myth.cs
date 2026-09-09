namespace ProjectPQ.Scripts.Games.Items.Relics.Rarities;

public record class Myth : RelicRarity
{
    protected override string BaseName => "신화";

    public override long DailyGoldLimit => 2_000L;
}