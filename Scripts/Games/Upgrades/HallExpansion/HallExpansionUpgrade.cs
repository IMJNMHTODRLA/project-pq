namespace ProjectPQ.Scripts.Games.Upgrades.HallExpansion;

public class HallExpansionUpgrade : Upgrade
{
    protected override string BaseName => "전시관 확장";
    protected override string BaseDescription => "설명 추후에 추가";
    protected override string BaseEffect => "추후에 추가";

    protected override long BaseCost => 2_000L;
    protected override float BaseCostMultiple => 1.85f;

    public override int MaxLevel => 8;
    public override int UnlockDate => 3;
}