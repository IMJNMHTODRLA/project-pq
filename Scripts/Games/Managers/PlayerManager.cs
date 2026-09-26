using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Entities.Players;
using ProjectPQ.Scripts.Games.Relics;

namespace ProjectPQ.Scripts.Games.Managers;

[JsonTypeId(0x5926_E8BF_117E_1DBE)]
public sealed class PlayerManager : GameService, IServiceDirect<PlayerManager>
{
    public static PlayerManager Self => GameManager.Self.Player;

    [JsonProperty]
    public Item[] Inventory
    {
        get => Player.Inventory.ToArray();
        init => Player.Inventory = value;
    }

    [JsonProperty]
    public long Gold
    {
        get => Player.Gold;
        init => Player.Gold = value;
    }

    public Player Player { get; }

    public PlayerManager()
    {
        Player = SceneLoader.Load<Player, EmptyArgs>();
    }
}