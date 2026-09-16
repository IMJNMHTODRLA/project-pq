using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0xFECFAE0661C56809)]
public class DetailAppraisal : RelicAppraisal
{
    protected override string BaseName => LangKey.RELIC_APPRAISAL_DETAIL_APPRAISAL_NAME.Translate();

    public override double Rate => 0.9;
}