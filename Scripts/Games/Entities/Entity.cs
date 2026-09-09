using Godot;

namespace ProjectPQ.Scripts.Games.Entities;

public abstract partial class Entity : CharacterBody2D
{
    public abstract string EntityName { get; set; }

    public abstract bool IsDead();

    public override void _Ready()
    {
        ReadyProcess();
    }

    public override void _PhysicsProcess(double delta)
    {
        if (TickManager.Self.IsPause)
            return;

        if (IsDead())
        {
            Despawn();
            return;
        }

        TickProcess(delta);
    }

    public virtual void Despawn()
    {
        QueueFree();
    }

    public abstract void ReadyProcess();
    public abstract void TickProcess(double delta);
}