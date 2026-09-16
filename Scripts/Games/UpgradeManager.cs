using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Upgrades.HallExpansion;

namespace ProjectPQ.Scripts.Games;

[JsonTypeId(0x17CA357E9D72A615)]
public sealed class UpgradeManagerSaveData(
    HallExpansionUpgrade? he = null
)
{
    [JsonProperty]
    public HallExpansionUpgrade HallExpansion { get; } = he ?? new();
}

public sealed partial class UpgradeManager : Singleton<UpgradeManager>
{
    public HallExpansionUpgrade HallExpansion { get; private set; } = new();

    public void Reset()
    {
        HallExpansion = new();
    }

    public bool GameStart(UpgradeManagerSaveData? saveData = null)
    {
        Reset();

        if (saveData != null)
        {
            HallExpansion = saveData.HallExpansion;
        }

        return true;
    }
}