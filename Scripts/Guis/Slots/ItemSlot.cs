using Godot;
using ProjectPQ.Scripts.Games.Relics;

namespace ProjectPQ.Scripts.Guis.Slots;

public readonly record struct ItemSlotArgs(
    Item Item
) : ISceneArgs;

public partial class ItemSlot : PanelContainer, IScene<ItemSlotArgs>
{
    public static string ScenePath => "res://Scenes/Guis/Slots/ItemSlot.tscn";

    [Export] private TextureRect _texture = null!;
    private Item _item = Item.Empty;

    public void SceneInit(ItemSlotArgs args)
    {
        _item = args.Item ?? Item.Empty;
    }

    public override void _Ready()
    {
        base._Ready();

        if (_item.IsEmpty())
            _texture.Texture = null;
        else
            _texture.Texture = _item.Icon;
    }
}