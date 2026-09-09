using Godot;

namespace ProjectPQ.Scripts.Games.Entities.Ables.Movables;

public interface IMovable
{
    public void OnMove(Vector2 direction);
    public void StopMove();
}