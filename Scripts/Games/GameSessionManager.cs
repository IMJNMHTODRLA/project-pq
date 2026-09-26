using System;
using System.Numerics;
using System.Threading.Tasks;
using ProjectPQ.Scripts.Games.Managers;

namespace ProjectPQ.Scripts.Games;

public partial class GameSessionManager : Singleton<GameSessionManager>
{
    public enum SessionResult
    {
        SaveNotFound,
        InvalidSave,
        AlreadyRunning,
        Complete
    }

    public enum SaveResult
    {
        NotRunning,
        Complete
    }

    private GameManager? _current = null;

    public GameManager? Current => _current;
    public GameManager CurrentNotSafe => _current!;

    public bool IsGameStart => _current != null;
    public bool IsPause
    {
        get => !IsGameStart || field;
        set;
    } = true;

    public void EndSession()
    {
        _current?.OnExit();
        _current = null;
    }

    public async Task<SessionResult> LoadSession(GameId id)
    {
        string? rawSaveData = await AppDataUtils.ReadTextAsync($"save_data/{id}/{id}.json");
        if (rawSaveData == null) return SessionResult.SaveNotFound;

        GameManager? game = JsonUtils.Deserialize<GameManager>(rawSaveData);
        if (game == null) return SessionResult.InvalidSave;

        return await StartSession(game);
    }

    public async Task<SessionResult> NewSession() =>
        await StartSession(new());
    
    private async Task<SessionResult> StartSession(GameManager game)
    {
        if (IsGameStart) return SessionResult.AlreadyRunning;

        _current = game;
        _current.OnLoad();

        return SessionResult.Complete;
    }

    public async Task<SaveResult> SaveSession()
    {
        if (_current == null) return SaveResult.NotRunning;

        GameId id = _current.Id;
        string rawSaveData = JsonUtils.Serialize(_current);

        await AppDataUtils.WriteTextAsync($"save_data/{id}/{id}.json", rawSaveData);
        return SaveResult.Complete;
    }

    protected override void OnTick(double delta)
    {
        if (IsPause) return;

        _current?.OnTick(delta);
    }
}
