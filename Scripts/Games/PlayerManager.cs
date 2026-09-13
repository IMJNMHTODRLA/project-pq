using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Entities.Players;
using ProjectPQ.Scripts.Games.Relics;

namespace ProjectPQ.Scripts.Games;

[method: JsonConstructor]
[JsonTypeId(0x5926_E8BF_117E_1DBE)]
public sealed class PlayerManagerSaveData(
    Item[]? inventory = null,
    long? gold = null
)
{
    [JsonProperty] public Item[] Inventory { get; } = inventory ?? Player.DEFAULT_INVENTORY;
    [JsonProperty] public long Gold { get; } = gold ?? Player.DEFAULT_GOLD;
}

public sealed partial class PlayerManager : Singleton<PlayerManager>
{
    public Player Player { get; private set; } = null!;

    public void Reset()
    {
        Player?.QueueFree();

        Player = SceneLoader.Load<Player, EmptyArgs>();
        Player.DetachNode();
    }

    public bool GameStart(PlayerManagerSaveData? saveData = null)
    {
        Reset();

        if (saveData != null)
        {
            Player.Gold = saveData.Gold;
            Player.Inventory = saveData.Inventory;
        }

        return true;
    }

    protected override void OnAutoload() => Reset();
    protected override void OnTick(double delta) {}
}