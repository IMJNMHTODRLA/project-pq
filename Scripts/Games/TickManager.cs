using Godot;
using Newtonsoft.Json;

namespace ProjectPQ.Scripts.Games;

[JsonTypeId(0xC6CF_786A_249A_C432)]
public sealed class TickManagerSaveData(long? gameTick = null)
{
    [JsonProperty]
    public long GameTick { get; } = gameTick ?? TickManager.DEFAULT_GAME_TICK;
}

// Default
public sealed partial class TickManager
{
    public const bool DEFAULT_IS_START = false;
    public const bool DEFAULT_IS_PAUSE = true;

    public const long DEFAULT_GAME_TICK = 0L;
}

public sealed partial class TickManager : Singleton<TickManager>
{
    private const long DAILY_TICK = 36_000L;

    public bool IsStart { get; private set; } = DEFAULT_IS_START;
    public bool IsPause { get; set; } = DEFAULT_IS_PAUSE;

    public long GameTick { get; private set; } = DEFAULT_GAME_TICK;
    public long GameDay => GameTick / DAILY_TICK;

    [Signal] public delegate void NextDayEventHandler(long day);
    [Signal] public delegate void MidnightEventHandler();

    public void Reset()
    {
        IsStart = DEFAULT_IS_START;
        IsPause = DEFAULT_IS_PAUSE;

        GameTick = DEFAULT_GAME_TICK;
    }

    public bool GameStart(TickManagerSaveData? saveData = null)
    {
        Reset();

        if (saveData != null)
        {
            GameTick = saveData.GameTick;
        }

        StartTick();
        return true;
    }

    protected override void OnAutoload() {}

    protected override void OnTick(double delta)
    {
        if (!IsStart || IsPause) return;

        long beforeDay = GameDay;

        GameTick++;

        if (beforeDay != GameDay)
        {
            EmitSignal(SignalName.NextDay, GameDay);
            return;
        }
        
    }

    private void StartTick()
    {
        IsStart = true;
        IsPause = false;
    }
}