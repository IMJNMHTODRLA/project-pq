namespace ProjectPQ.Scripts.Games.Upgrades.ExpansionStorage;

public class ExpansionStorageUpgrade : Upgrade<int>
{
    protected override string BaseName => "수장고 확장";
    protected override string BaseDescription => "설명 추가하기ㅇㅇ";
    protected override string BaseEffect => "추후에 추가";

    protected override long BaseCost => 1_000;
    protected override float BaseCostMultiple => 1.7f;

    public override int MaxLevel => int.MaxValue;
    public override int UnlockDate => 1;

    public override int EffectValue => 9 + Level;
}