namespace ProjectPQ.Scripts.Games.Relics.Appraisals;

[JsonTypeId(0x17DF337A635B3308)]
public class Unidentified : RelicAppraisal
{
    protected override string BaseName => "미확인";

    public override double Rate => 0.1;
}