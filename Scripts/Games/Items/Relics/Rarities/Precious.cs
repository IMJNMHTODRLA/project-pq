namespace ProjectPQ.Scripts.Games.Items.Relics.Rarities;

public record class Precious : RelicRarity
{
    protected override string BaseName => "귀중한";

    public override long DailyGoldLimit => 180L;
}