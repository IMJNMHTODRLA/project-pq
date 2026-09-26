using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Upgrades.HallExpansion;

namespace ProjectPQ.Scripts.Games.Managers;

[JsonTypeId(0x17CA357E9D72A615)]
public class UpgradeManager : GameService, IServiceDirect<UpgradeManager>
{
    public static UpgradeManager Self => GameManager.Self.Upgrade;

    [JsonProperty]
    public HallExpansionUpgrade HallExpansion { get; init; } = new();
}