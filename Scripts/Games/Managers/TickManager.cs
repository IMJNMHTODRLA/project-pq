using Newtonsoft.Json;

namespace ProjectPQ.Scripts.Games.Managers;

[JsonTypeId(0xC6CF_786A_249A_C432)]
public sealed class TickManager : GameService, IServiceDirect<TickManager>
{
    public static TickManager Self => GameManager.Self.Tick;

    public const long DAILY_TICK = 36_000L;

    public delegate void NextDayListener(long day);
    public event NextDayListener? NextDay;

    public delegate void MidnightListener();
    public event MidnightListener? Midnight;

    [JsonProperty]
    public long GameTick { get; private set; } = 0L;
    public long GameDay => GameTick / DAILY_TICK;

    public override void OnTick(double delta)
    {
        base.OnTick(delta);

        long beforeDay = GameDay;

        GameTick++;

        if (beforeDay != GameDay)
            NextDay?.Invoke(GameDay);
    }
}