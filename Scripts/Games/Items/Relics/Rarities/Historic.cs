namespace ProjectPQ.Scripts.Games.Items.Relics.Rarities;

public record class Historic : RelicRarity
{
    public override string Id { get; } = "historic";

    protected override string Color { get; } = ColorUtils.DarkGray; //TODO: 추후에 그거 ㅅ ㅜ정
    protected override string BaseName { get; } = "유서 깊은";

    public override long DailyGoldLimit { get; } = 600L;
}