using System.Collections.Generic;
using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Items.Relics;

namespace ProjectPQ.Scripts.Games.Maps.Museums;

[RegisterMapData]
[method: JsonConstructor]
public partial class MuseumData(List<Relic> regRelics) : MapData
{
    public MuseumData() : this(regRelics: [])
    {
    }

    private const int MAX_REGISTER_RELIC = 5;

    private readonly List<Relic> _registeredRelics = regRelics; 

    public bool RegisterRelic(Relic relic)
    {
        if (_registeredRelics.Count >= MAX_REGISTER_RELIC)
            return false;

        _registeredRelics.Add(relic);
        return true;
    }

    public bool UnRegisterRelic(int index, out Relic? relic)
    {
        relic = _registeredRelics.GetOrNull(index);
        if (relic == null)
            return false;

        _registeredRelics.RemoveAt(index);
        return true;
    }

    public IReadOnlyList<Relic> GetAllRegisterRelic() =>
        _registeredRelics;
}