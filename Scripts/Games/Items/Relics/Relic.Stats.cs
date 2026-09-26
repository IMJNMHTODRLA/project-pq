using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Relics;

namespace ProjectPQ.Scripts.Games.Items.Relics;

public abstract partial class Relic
{
    public RelicRarity Rarity { get; init; } = rarity;

    [JsonProperty]
    public RelicPreservation Preservation { get; init; } = preservation ?? RelicPreservation.Piece;
    
    [JsonProperty]
    public RelicAppraisal Appraisal { get; init; } = appraisal ?? RelicAppraisal.Unidentified;

    public long DailyGold => (long) (
        Rarity.DailyGold *
        Preservation.Rate *
        Appraisal.Rate
    );
}