using System;
using System.Linq;
using ProjectPQ.Scripts.Games.Entities.Players;
using ProjectPQ.Scripts.Games.Items;
using ProjectPQ.Scripts.Games.Managers;

namespace ProjectPQ.Scripts.Games.Upgrades;

public abstract class ItemCostUpgrade<TValue> : Upgrade<TValue>
{
    protected abstract (Func<Item, bool>, int)[] BaseItemCost { get; }
    public (Func<Item, bool>, int)[] ItemCost =>
    [
        ..BaseItemCost
        .Select((x) => (
            x.Item1,
            (int)(x.Item2 * Multiple)
        ))
    ];

    public override UpgradeResult Pay()
    {
        Player player = PlayerManager.Self.Player;

        if (!IsUnlocked()) return UpgradeResult.NotUnlocked;
        if (IsMaxLevel()) return UpgradeResult.MaxLevel;
        if (!player.CanAffordGold(Cost)) return UpgradeResult.NotEnoughCost;

        foreach (var (condition, amount) in ItemCost)
            if (!player.HasItem(condition, amount))
                return UpgradeResult.NotEnoughCost;
        
        foreach (var (condition, amount) in ItemCost)
            player.RemoveItem(condition, amount);
        
        player.TrySpendGold(Cost);
        AddLevel();

        return UpgradeResult.Success;
    }
}