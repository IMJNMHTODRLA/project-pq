using Godot;

namespace ProjectPQ.Scripts.Games.Entities.Players;

public partial class Player
{
    [Export] private RemoteTransform2D _remote = null!;

    public void ChangeCamera(Camera2D camera)
    {
        _remote.RemotePath = camera.GetPath();
    }
}