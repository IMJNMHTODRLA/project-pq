namespace ProjectPQ.Scripts.Games.Items.Relics;

//보존도
public abstract record class RelicPreservation
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
    /// 보존도 배율.
    /// </summary>
    public abstract double PreservationRate { get; }
}
