namespace ProjectPQ.Scripts.Games.Upgrades.HiringArchaeo;

public class HiringArchaeoUpgrade : Upgrade<double>
{
    protected override string BaseName => "고고학자 고용";
    protected override string BaseDescription => "설명 추가하기";
    protected override string BaseEffect => "추후에 추가";

    protected override long BaseCost => 1000;
    protected override float BaseCostMultiple => 2.0f;

    public override int MaxLevel => 10;
    public override int UnlockDate => 3;

    public override double EffectValue => Level;
}