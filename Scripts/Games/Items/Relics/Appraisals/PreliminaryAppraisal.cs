namespace ProjectPQ.Scripts.Games.Items.Relics.Appraisals;

public record class PreliminaryAppraisal : RelicAppraisal
{
    protected override string BaseName { get; } = "기초 감정";

    public override double AppraisalRate { get; } = 0.8;
}
