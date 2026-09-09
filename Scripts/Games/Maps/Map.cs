using Godot;
using ProjectPQ.Scripts.Games.Entities.Players;

namespace ProjectPQ.Scripts.Games.Maps;

public abstract partial class Map : Node2D
{
    [Export] private Marker2D PlayerSpawnPos { get; set; } = null!;
    [Export] private Node2D WorldNode = null!;
    [Export] private Camera2D Camera = null!;

    public override void _Ready()
    {
        ActivateMap();
        TreeExiting += DeactivateMap;
    }

    protected virtual void DeactivateMap()
    {
        Player player = PlayerManager.Self.Player;

        player.DetachNode();
    }

    protected virtual void ActivateMap()
    {
        WorldNode.YSortEnabled = true;

        Player player = PlayerManager.Self.Player;

        AddWorld(player);
        player.GlobalPosition = PlayerSpawnPos.GlobalPosition;

        player.ChangeCamera(Camera);

        MapManager.Self.CurrentMap = this;
    }

    public virtual void AddWorld(Node2D entry)
    {
        WorldNode.AttachNode(entry);
    }
}