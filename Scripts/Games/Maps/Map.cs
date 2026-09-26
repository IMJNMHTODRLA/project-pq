using Godot;
using ProjectPQ.Scripts.Games.Entities.Players;
using ProjectPQ.Scripts.Games.Managers;

namespace ProjectPQ.Scripts.Games.Maps;

public abstract partial class Map : Node2D
{
    [Export] private Marker2D _playerSpawnPos = null!;
    [Export] private Camera2D _camera = null!;

    private Node2D _worldNode = new() { YSortEnabled = true };
    private Node2D _overlayNode = new();

    public abstract MapData LinkMapData { get; }

    public override void _Ready()
    {
        AddChild(_worldNode);
        AddChild(_overlayNode);

        TreeExiting += DeactivateMap;
        ActivateMap();

        MapManager.Self.CurrentMap = this;
    }

    protected virtual void DeactivateMap()
    {
        Player player = PlayerManager.Self.Player;
        player.DetachNode();
    }

    protected virtual void ActivateMap()
    {
        Player player = PlayerManager.Self.Player;
        player.GlobalPosition = _playerSpawnPos.GlobalPosition;

        AddWorld(player);
        player.ChangeCamera(_camera);
    }

    public void AddWorld(Node2D entry) => _worldNode.AttachNode(entry);
    public void AddOverlay(Node2D entry) => _overlayNode.AttachNode(entry);

    //public void RemoveWorld(Node2D entry) => entry.DetachNode();
    //public void RemoveOverlay(Node2D entry) => entry.DetachNode();
    // public virtual void QueueFreeWorld(Node2D entry)
    // {
    //     RemoveWorld(entry);
    //    entry.SafeQueueFree();
    // }
    // 중복 API WTF;;
}