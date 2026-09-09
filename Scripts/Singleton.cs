using System;
using Godot;

namespace ProjectPQ.Scripts;

public abstract partial class Singleton<T> : Node
    where T : Singleton<T>
{
    public static bool IsInstance => Self != null;
    public static T Self { get; private set; } = null!;

    public override void _EnterTree()
    {
        if (this is not T)
            LogUtils.ThrowError<InvalidOperationException>(
                $"Singleton에서 T가 {GetType().Name}가 아닌 {typeof(T).Name}임."
            );

        if (IsInstance && Self != this)
        {
            GD.PrintErr($"[{typeof(T).Name}] 이미 싱글톤 인스턴스가 존재하여 중복 노드를 제거합니다.");
            QueueFree();
            return;
        }

        Self ??= (T) (object) this;
        
        OnAutoload();
    }

    public override void _PhysicsProcess(double delta)
    {
        OnTick(delta);
    }

    public override void _ExitTree()
    {
        OnExit();
    }

    protected virtual void OnAutoload() {}
    protected virtual void OnExit() {}

    protected virtual void OnTick(double delta) {}
}