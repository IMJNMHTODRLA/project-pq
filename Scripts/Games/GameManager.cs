using System;
using System.Threading.Tasks;
using Godot;
using Newtonsoft.Json;
using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games;

[JsonTypeId(0xF9E9_CE81_F466_E8E1)]
public sealed class GameManagerSaveData(ulong? id)
{
    public bool IsInvalid =>
        MapManager == null ||
        PlayerManager == null ||
        TickManager == null;

    public ulong Id { get; } = id ?? throw new ArgumentException("id is NOT FOUND!!!!");
    [JsonProperty] public ulong Version { get; init; } = GameMetadata.Version;

    [JsonProperty] public MapManagerSaveData? MapManager { get; init; } = new();
    [JsonProperty] public PlayerManagerSaveData? PlayerManager { get; init; } = new();

    [JsonProperty] public TickManagerSaveData? TickManager { get; init; } = new();
}

public enum GameStartResult
{
    Success,
    
    NotInstance,
    SettingNotLoad,
    IsGameLoading,
    SaveNotFound,
    InvalidSaveData,
}

public sealed partial class GameManager : Singleton<GameManager>
{
    // GameManager
    // ======== 의존성 ========
    // SettingManager, MapManager
    // PlayerManager, TickManager

    public ulong? Id { get; private set; } = null;
    public ulong Version { get; private set; } = GameMetadata.Version;

    public async Task<GameStartResult> NewGame()
    {
        if (!IsInstance) return GameStartResult.NotInstance;
        if (!SettingManager.Self.IsLoad) return GameStartResult.SettingNotLoad;

        Id = Random.Shared.NextUInt64();
        Version = GameMetadata.Version;

        MapManager.Self.GameStart();
        PlayerManager.Self.GameStart();

        TickManager.Self.GameStart();

        return GameStartResult.Success;
    }

    public async Task<GameStartResult> ContinueGame(ulong id)
    {
        if (!IsInstance) return GameStartResult.NotInstance;
        if (!SettingManager.Self.IsLoad) return GameStartResult.SettingNotLoad;

        GameManagerSaveData? saveData =
            JsonUtils.Deserialize<GameManagerSaveData>(
                await AppDataUtils.ReadTextAsync(
                    $"save_data/{id}/{id}.json"
            ));

        if (saveData.IsNull) return GameStartResult.SaveNotFound;
        if (saveData.IsInvalid) return GameStartResult.InvalidSaveData;

        Id = saveData.Id;
        Version = saveData.Version;

        MapManager.Self.GameStart(saveData?.MapManager);
        PlayerManager.Self.GameStart(saveData?.PlayerManager);

        TickManager.Self.GameStart(saveData?.TickManager);

        return GameStartResult.Success;
    }

    protected override async void OnAutoload()
    {
        await SettingManager.Self.InitTask;

        GD.Print(Tr(LangKey.HELLO));
    }

    protected override void OnTick(double delta)
    {
    }
}