using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Managers;
using ProjectPQ.Scripts.Games.Relics.Relics;

namespace ProjectPQ.Scripts.Games.Maps.Museums;

[RegisterMapData]
public class MuseumData : MapData
{
    [JsonProperty]
    public IReadOnlyList<Relic?> RegisterRelics
    {
        get => _registerRelics;
        init => _registerRelics = value?.ToList() ?? [];
    }

    private long _maxRegisterRelic => UpgradeManager.Self.HallExpansion.EffectValue;
    private readonly List<Relic?> _registerRelics = []; 

    public bool RegisterRelic(int index, Relic relic)
    {
        if (index >= _maxRegisterRelic)
            return false;

        _registerRelics.SetOrFill(index, relic, null);
        return true;
    }

    public bool UnRegisterRelic(int index, out Relic? relic)
    {
        relic = _registerRelics.GetOrNull(index);
        _registerRelics.SetOrSkip(index, null);

        return relic != null;
    }
}