namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0x187E9EFBF6557D65)]
public class PreliminaryAppraisal : RelicAppraisal
{
    protected override string BaseName => "기초 감정";

    public override double Rate => 0.8;
}
