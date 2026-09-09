namespace ProjectPQ.Scripts.Games.Items.Relics.Appraisals;

public record class Unidentified : RelicAppraisal
{
    protected override string BaseName { get; } = "미확인";

    public override double AppraisalRate { get; } = 0.1;
}