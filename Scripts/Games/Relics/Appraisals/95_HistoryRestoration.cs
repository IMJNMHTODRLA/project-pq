using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0xA6C6CC479048312A)]
public class HistoryRestoration : RelicAppraisal
{
    protected override string BaseName => LangKey.RELIC_APPRAISAL_HISTORY_RESTORATION_NAME.Translate();

    public override double Rate => 1.0;
}