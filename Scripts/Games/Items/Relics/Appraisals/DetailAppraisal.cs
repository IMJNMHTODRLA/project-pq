namespace ProjectPQ.Scripts.Games.Items.Relics.Appraisals;

public record class DetailAppraisal : RelicAppraisal
{
    protected override string BaseName { get; } = "정밀 감정";

    public override double AppraisalRate { get; } = 0.9;
}