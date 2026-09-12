namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0xA6C6CC479048312A)]
public class HistoryRestoration : RelicAppraisal
{
    protected override string BaseName => "역사 복원";

    public override double Rate => 1.0;
}