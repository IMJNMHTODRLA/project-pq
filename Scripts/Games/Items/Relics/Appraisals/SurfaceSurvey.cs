namespace ProjectPQ.Scripts.Games.Items.Relics.Appraisals;

public record class SurfaceSurvey : RelicAppraisal
{
    protected override string BaseName { get; } = "표면 조사";

    public override double AppraisalRate { get; } = 0.7;
}