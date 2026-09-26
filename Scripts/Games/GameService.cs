namespace ProjectPQ.Scripts.Games;

public interface IServiceDirect<T>
    where T : GameService, IServiceDirect<T>
{
    public abstract static T Self { get; }
}

public abstract class GameService
{
    public virtual void OnLoad() {}
    public virtual void OnTick(double delta) {}
    public virtual void OnExit() {}
}