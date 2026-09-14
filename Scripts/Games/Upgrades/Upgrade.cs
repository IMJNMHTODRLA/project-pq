using System;
using ProjectPQ.Scripts.Games.Entities.Players;

namespace ProjectPQ.Scripts.Games.Upgrades;

public enum UpgradeResult
{
    Success,
    MaxLevel,
    NotEnoughGold
}

public abstract class Upgrade
{
    protected abstract string BaseName { get; }
    protected abstract string BaseDescription { get; }
    protected abstract string BaseEffect { get; }

    protected abstract long BaseCost { get; }
    protected abstract float BaseCostMultiple { get; }

    public abstract int MaxLevel { get; }
    public virtual int UnlockDate { get; } = 0;

    public long Level { get; private set; } = 0;
    public bool CanAddLevel() => MaxLevel > Level;
    public bool AddLevel()
    {
        bool canAddLevel = CanAddLevel();
        if (canAddLevel) Level++;
        return canAddLevel;
    }

    public UpgradeResult AddLevelAndCostDec()
    {
        Player player = PlayerManager.Self.Player;

        if (!CanAddLevel()) return UpgradeResult.MaxLevel;
        if (!player.CanAffordGold(Cost)) return UpgradeResult.NotEnoughGold;
        
        AddLevel();
        player.SpendGold(Cost);

        return UpgradeResult.Success;
    }

    // BaseCost * (BaseCostMultiple ^ Level)
    public long Cost => BaseCost * (long) Math.Pow(BaseCostMultiple, Level);

    public bool IsUnlocked() =>
        TickManager.Self.GameDay >= UnlockDate;
}