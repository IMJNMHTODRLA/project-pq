using System.Collections.Generic;
using Godot;
using ProjectPQ.Scripts.Games.Entities.Players;

namespace ProjectPQ.Scripts.Games.Maps;

public abstract partial class Map : Node2D
{
    private readonly List<Node2D> _worldNodes = [];

    [Export] private Marker2D PlayerSpawnPos { get; set; } = null!;
    [Export] private Node2D WorldNode = null!;
    [Export] private Camera2D Camera = null!;

    public abstract MapData LinkMapData { get; }

    public override void _Ready()
    {
        TreeExiting += DeactivateMap;
        ActivateMap();
    }

    protected virtual void DeactivateMap()
    {
        Player player = PlayerManager.Self.Player;
        RemoveWorld(player);

        foreach (Node entry in _worldNodes)
            entry.QueueFree();
        
        _worldNodes.Clear();
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

    public virtual void RemoveWorld(Node2D entry)
    {
        entry.DetachNode();
        _worldNodes.Remove(entry);
    }

    public virtual void AddWorld(Node2D entry)
    {
        WorldNode.AttachNode(entry);
        _worldNodes.Add(entry);
    }
}