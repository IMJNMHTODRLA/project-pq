using System;
using System.Linq;
using ProjectPQ.Scripts.Games.Items;

namespace ProjectPQ.Scripts.Games.Entities.Players;

public partial class Player
{
    public static Item[] DEFAULT_INVENTORY => [..Enumerable.Repeat(Item.Empty, 27)];

    private Item[] _inventory = DEFAULT_INVENTORY;
    public ReadOnlySpan<Item> Inventory
    {
        get => _inventory;
        set => _inventory = value.ToArray();
    }

    public bool AddItem(Item item, int times = 1)
    {
        if (item.IsEmpty())
            return false;

        for (int i = 0; i < _inventory.Length; i++)
        {
            if (_inventory[i].IsNotEmpty())
                continue;

            if (SetItem(i, item))
                times--;
            
            if (times <= 0)
                return true;
        }

        return false;
    }

    public bool RemoveItem(Item item, int times = 1)
    {
        if (item.IsEmpty())
            return false;

        for (int i = 0; i < _inventory.Length; i++)
        {
            if (!item.Equals(_inventory[i]))
                continue;

            if (SetItem(i, Item.Empty))
                times--;
            
            if (times <= 0)
                return true;
        }

        return false;
    }

    public bool SetItem(int index, Item item) =>
        _inventory.SetOrSkip(index, item.Clone());

    public bool IsEmpty(int index) =>
        _inventory.GetOrNull(index)?.IsEmpty() ?? false;
}