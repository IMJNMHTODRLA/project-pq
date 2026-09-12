namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0x5E59C329873CF6F4)]
public class SurfaceSurvey : RelicAppraisal
{
    protected override string BaseName => "표면 조사";

    public override double Rate => 0.7;
}