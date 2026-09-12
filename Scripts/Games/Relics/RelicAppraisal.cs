using ProjectPQ.Scripts.Games.Relics.Appraisals;

namespace ProjectPQ.Scripts.Games.Relics;

//감정도
public abstract partial class RelicAppraisal
{
    protected abstract string BaseName { get; }

    public string Name => $"[color={ColorUtils.DarkGray}][[/color][color={ColorUtils.White}]{BaseName}[/color][color={ColorUtils.DarkGray}]][/color]";

    public abstract double Rate { get; }
}

public abstract partial class RelicAppraisal
{
    public static readonly HistoryRestoration HistoryRestoration = new();
    public static readonly DetailAppraisal DetailAppraisal = new();
    public static readonly PreliminaryAppraisal PreliminaryAppraisal = new();
    public static readonly SurfaceSurvey SurfaceSurvey = new();
    public static readonly Unidentified Unidentified = new();
}