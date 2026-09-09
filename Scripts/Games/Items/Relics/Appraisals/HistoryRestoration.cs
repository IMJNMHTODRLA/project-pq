namespace ProjectPQ.Scripts.Games.Items.Relics.Appraisals;

public record class HistoryRestoration : RelicAppraisal
{
    protected override string BaseName { get; } = "역사 복원";

    public override double AppraisalRate { get; } = 1.0;
}