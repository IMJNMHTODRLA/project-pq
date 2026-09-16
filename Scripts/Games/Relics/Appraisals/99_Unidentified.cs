using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0x17DF337A635B3308)]
public class Unidentified : RelicAppraisal
{
    protected override string BaseName => LangKey.RELIC_APPRAISAL_UNIDENTIFIED_NAME.Translate();

    public override double Rate => 0.1;
}