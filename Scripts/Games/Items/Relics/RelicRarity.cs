namespace ProjectPQ.Scripts.Games.Items.Relics;

//희귀도
public abstract record class RelicRarity
{
    protected abstract string Color { get; }
    protected abstract string BaseName { get; }

    public string Name
    {
        get
        {
            string startPrefix = $"[color={ColorUtils.White}][[/color]";
            string endPrefix = $"[color={ColorUtils.White}]][/color]";

            string colorName = $"[color={Color}]{BaseName}[/color]";

            return startPrefix + colorName + endPrefix;
        }
    }

    /// <summary>
    /// 최대 일일 골드 수익량
    /// </summary>
    public abstract long DailyGoldLimit { get; }
}