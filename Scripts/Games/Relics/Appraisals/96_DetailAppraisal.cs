namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0xFECFAE0661C56809)]
public class DetailAppraisal : RelicAppraisal
{
    protected override string BaseName => "정밀 감정";

    public override double Rate => 0.9;
}