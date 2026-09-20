using Godot;
using ProjectPQ.Scripts.Games;
using ProjectPQ.Scripts.Games.Entities.Players;
using ProjectPQ.Scripts.Games.Relics;

namespace ProjectPQ.Scripts.Guis.Inventories;

public partial class Inventory : Control, IScene<EmptyArgs>
{
    public static string ScenePath => "res://Scenes/Guis/Inventories/Inventory.tscn";

    [Export] private Panel _inventoryPanel = null!;
    
    [Export] private Control _firstItemPos = null!;
    [Export] private int _itemSize = 16; // 아이템 가로 세로 길이(px)
    [Export] private int _widthPadding = 5; // 아이템과 아이템 사이의 가로 거리(px)
    [Export] private int _lengthPadding = 5; // 아이템과 아이템 사이의 세로 거리(px)
    [Export] private int _perLine = 9; // 한 줄에 몇개
    [Export] private int _totalLines = 3; // 총 줄 개수

    public override void _Ready()
    {
        base._Ready();

        Player player = PlayerManager.Self.Player;

        Vector2 pos = _firstItemPos.GlobalPosition;
        int itemIndex = 0;

        for (int y = 0; y < _totalLines; y++)
        {
            for (int x = 0; x < _perLine; x++)
            {
                Item item = player.Inventory.GetOrNull(itemIndex) ?? Item.Empty;
                Texture2D? icon = item.IsEmpty() ? null : item.Icon;

                TextureRect texture = new()
                {
                    GlobalPosition = pos,
                    Texture = icon
                };

                _inventoryPanel.AttachNode(texture);

                pos.X += _itemSize + _widthPadding;
                itemIndex++;
            }

            pos.X = _firstItemPos.GlobalPosition.X;
            pos.Y += _itemSize + _lengthPadding;
        }
    }
}