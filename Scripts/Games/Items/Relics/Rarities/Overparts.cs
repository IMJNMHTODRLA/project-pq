namespace ProjectPQ.Scripts.Games.Items.Relics.Rarities;

public record class Overparts : RelicRarity
{
    protected override string BaseName => "오버파츠";

    public override long DailyGoldLimit => 3_800L;
}