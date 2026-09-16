using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0x5E59C329873CF6F4)]
public class SurfaceSurvey : RelicAppraisal
{
    protected override string BaseName => LangKey.RELIC_APPRAISAL_SURFACE_SURVERY_NAME.Translate();

    public override double Rate => 0.7;
}