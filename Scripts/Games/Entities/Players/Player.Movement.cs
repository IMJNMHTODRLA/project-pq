using Godot;
using ProjectPQ.Scripts.Games.Entities.Ables.Movables;

namespace ProjectPQ.Scripts.Games.Entities.Players;

public partial class Player : IMovable
{
    private MoveComponent _move = null!;

    private void InitializeMove()
    {
        _move = new(this, 100.0f);
    }

    public void OnMove(Vector2 direction) => _move.OnMove(direction);
    public void StopMove() => _move.StopMove();

    private void ProcessMovement()
    {
        Vector2 inputDirection = Input.GetVector(
            InputMap.A,
            InputMap.D,
            InputMap.W,
            InputMap.S
        );

        OnMove(inputDirection);
        
        MoveAndSlide();
    }
}