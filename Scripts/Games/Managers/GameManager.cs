using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Maps.Museums;

namespace ProjectPQ.Scripts.Games.Managers;

[JsonTypeId(0xF9E9_CE81_F466_E8E1)]
public sealed class GameManager : GameService, IServiceDirect<GameManager>
{
    public static GameManager Self => GameSessionManager.Self.CurrentNotSafe;

    [JsonProperty] public GameId Id { get; init; } = new();
    [JsonProperty] public GameVersion Version { get; init; } = GameMetadata.Version.Latest;

    [JsonProperty] public MapManager Map { get; init; } = new();
    [JsonProperty] public TickManager Tick { get; init; } = new();
    [JsonProperty] public PlayerManager Player { get; init; } = new();
    [JsonProperty] public UpgradeManager Upgrade { get; init; } = new();

    public override void OnLoad()
    {
        Map.OnLoad();
        Tick.OnLoad();
        Player.OnLoad();
        Upgrade.OnLoad();
    
        SceneLoader.Change<Museum, EmptyArgs>();
    }

    public override void OnTick(double delta)
    {
        Map.OnTick(delta);
        Tick.OnTick(delta);
        Player.OnTick(delta);
        Upgrade.OnTick(delta);
    }

    public override void OnExit()
    {
        Map.OnExit();
        Tick.OnExit();
        Player.OnExit();
        Upgrade.OnExit();
    }
}