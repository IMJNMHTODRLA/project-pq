using System.Collections.Generic;
using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Relics.Relics;

namespace ProjectPQ.Scripts.Games.Maps.Museums;

[RegisterMapData]
[method: JsonConstructor]
public class MuseumData(IReadOnlyList<Relic> regRelics) : MapData
{
    public MuseumData() : this(regRelics: [])
    {
    }

    private readonly long _maxRegisterRelic = 2 + UpgradeManager.Self.HallExpansion.Level;

    private readonly List<Relic> _registerRelics = [..regRelics]; 

    public bool RegisterRelic(Relic relic)
    {
        if (_registerRelics.Count >= _maxRegisterRelic)
            return false;

        _registerRelics.Add(relic);
        return true;
    }

    public bool UnRegisterRelic(int index, out Relic? relic)
    {
        relic = _registerRelics.GetOrNull(index);
        if (relic == null)
            return false;

        _registerRelics.RemoveAt(index);
        return true;
    }

    public IReadOnlyList<Relic> GetAllRegisterRelic() =>
        _registerRelics;
}