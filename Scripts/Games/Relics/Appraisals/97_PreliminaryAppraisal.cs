using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0x187E9EFBF6557D65)]
public class PreliminaryAppraisal : RelicAppraisal
{
    protected override string BaseName => LangKey.RELIC_APPRAISAL_PRELIMINARY_APPRAISAL_NAME.Translate();

    public override double Rate => 0.8;
}
