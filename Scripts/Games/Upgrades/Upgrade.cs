using System;
using ProjectPQ.Scripts.Games.Entities.Players;

namespace ProjectPQ.Scripts.Games.Upgrades;

public enum UpgradeResult
{
    Success,
    NotUnlocked,
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

    public bool IsUnlocked() => TickManager.Self.GameDay >= UnlockDate;
    public bool IsMaxLevel() => MaxLevel <= Level;
    public bool CanAddLevel() => !IsMaxLevel() && IsUnlocked();

    public bool AddLevel()
    {
        bool canAddLevel = CanAddLevel();
        if (canAddLevel) Level++;
        return canAddLevel;
    }

    public UpgradeResult AddLevelAndCostDec()
    {
        Player player = PlayerManager.Self.Player;

        if (!IsUnlocked()) return UpgradeResult.NotUnlocked;
        if (IsMaxLevel()) return UpgradeResult.MaxLevel;
        if (!player.CanAffordGold(Cost)) return UpgradeResult.NotEnoughGold;
        
        AddLevel();
        player.TrySpendGold(Cost);

        return UpgradeResult.Success;
    }

    // BaseCost * (BaseCostMultiple ^ Level)
    public long Cost => BaseCost * (long) Math.Pow(BaseCostMultiple, Level);
}