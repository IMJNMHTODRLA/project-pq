using Godot;

namespace ProjectPQ.Scripts.Games.Entities.Ables.Movables;

public class MoveComponent(CharacterBody2D body, float speed)
{
    public virtual float Speed { get; set; } = speed;

    public virtual void OnMove(Vector2 direction)
    {
        body.Velocity = direction * Speed;
    }

    public virtual void StopMove()
    {
        body.Velocity = Vector2.Zero;
    }
}