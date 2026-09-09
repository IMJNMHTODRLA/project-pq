namespace ProjectPQ.Scripts.Games.Items.Relics.Rarities;

public record class Rare : RelicRarity
{
    protected override string BaseName => "희귀한";

    public override long DailyGoldLimit => 350L;
}