using System;
using System.Linq;
using ProjectPQ.Scripts.Games.Items;
using ProjectPQ.Scripts.Games.Items.Empties;

namespace ProjectPQ.Scripts.Games.Entities.Players;

public partial class Player
{
    public static Item[] DEFAULT_INVENTORY => [..Enumerable.Repeat(Item.Empty, 27)];

    private Item[] _inventory = DEFAULT_INVENTORY;
    public ReadOnlySpan<Item> Inventory
    {
        get => _inventory;
        set
        {
            _inventory = value.ToArray();

            for (int i = 0; i < _inventory.Length; i++)
                _inventory[i] ??= Item.Empty;
        }
    }

    public bool SetItem(int index, Item item) => _inventory.SetOrSkip(index, item.Clone());
    public bool IsEmpty(int index) => _inventory.GetOrNull(index)?.IsEmpty() ?? true;

    public bool AddItem(Item item, int times = 1)
    {
        if (item.IsEmpty())
            return false;

        for (int i = 0; i < _inventory.Length; i++)
        {
            if (!_inventory[i].IsEmpty())
                continue;

            if (SetItem(i, item))
                times--;
            
            if (times <= 0)
                return true;
        }

        return false;
    }

    public bool RemoveItem(Item item, int times = 1) => RemoveItem(t => t.Equals(item), times);
    public bool RemoveItem(Func<Item, bool> predicate, int times = 1)
    {
        for (int i = 0; i < _inventory.Length; i++)
        {
            if (!predicate(_inventory[i])) continue;

            if (SetItem(i, Item.Empty))
                times--;
            
            if (times <= 0)
                return true;
        }

        return false;
    }

    public bool HasItem(Func<Item, bool> predicate, int times = 1)
    {
        if (times <= 0) return true;

        foreach (Item item in _inventory)
            if (predicate(item) && --times <= 0)
                return true;

        return false;
    }
}