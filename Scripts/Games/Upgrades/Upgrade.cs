using System;
using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Entities.Players;
using ProjectPQ.Scripts.Games.Items;
using ProjectPQ.Scripts.Games.Managers;

namespace ProjectPQ.Scripts.Games.Upgrades;

public enum UpgradeResult
{
    Success,
    NotUnlocked,
    MaxLevel,
    NotEnoughCost
}

public abstract class Upgrade<TValue>
{
    protected abstract string BaseName { get; }
    protected abstract string BaseDescription { get; }
    protected abstract string BaseEffect { get; }

    protected abstract long BaseCost { get; }
    protected abstract float BaseCostMultiple { get; }

    public abstract int MaxLevel { get; }
    public virtual int UnlockDate { get; } = 0;

    [JsonProperty]
    public int Level { get; private set; } = 0;
    public abstract TValue EffectValue { get; }

    public bool IsUnlocked() => TickManager.Self.GameDay >= UnlockDate;
    public bool IsMaxLevel() => MaxLevel <= Level;
    public bool CanAddLevel() => !IsMaxLevel() && IsUnlocked();

    public bool AddLevel()
    {
        bool canAddLevel = CanAddLevel();
        if (canAddLevel) Level++;
        return canAddLevel;
    }

    public virtual UpgradeResult Pay()
    {
        Player player = PlayerManager.Self.Player;

        if (!IsUnlocked()) return UpgradeResult.NotUnlocked;
        if (IsMaxLevel()) return UpgradeResult.MaxLevel;
        if (!player.CanAffordGold(Cost)) return UpgradeResult.NotEnoughCost;
        
        player.TrySpendGold(Cost);
        AddLevel();

        return UpgradeResult.Success;
    }

    // BaseCost * (BaseCostMultiple ^ Level)
    public long Cost => (long) (BaseCost * Multiple);
    public float Multiple => MathF.Pow(BaseCostMultiple, Level);
}