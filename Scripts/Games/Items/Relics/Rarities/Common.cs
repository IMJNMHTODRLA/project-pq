namespace ProjectPQ.Scripts.Games.Items.Relics.Rarities;

public record class Common : RelicRarity
{
    protected override string Color { get; } = ColorUtils.DarkGray;
    protected override string BaseName => "흔한";

    public override long DailyGoldLimit { get; } = 100L;
}