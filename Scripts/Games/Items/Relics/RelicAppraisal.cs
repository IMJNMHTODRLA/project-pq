namespace ProjectPQ.Scripts.Games.Items.Relics;

//감정도
public abstract record class RelicAppraisal
{
    protected abstract string BaseName { get; }

    public string Name
    {
        get
        {
            string startPrefix = $"[color={ColorUtils.DarkGray}][[/color]";
            string endPrefix = $"[color={ColorUtils.DarkGray}]][/color]";

            string colorName = $"[color={ColorUtils.White}]{BaseName}[/color]";

            return startPrefix + colorName + endPrefix;
        }
    }

    /// <summary>
    /// 감정도 배율.
    /// </summary>
    public abstract double AppraisalRate { get; }
}