using Newtonsoft.Json;
using ProjectPQ.Scripts.Games.Items.Relics.Appraisals;
using ProjectPQ.Scripts.Games.Items.Relics.Preservations;
using ProjectPQ.Scripts.Games.Items.Relics.Rarities;

namespace ProjectPQ.Scripts.Games.Items.Relics;

public abstract partial class Relic
{
    [JsonProperty] public RelicRarity Rarity { get; init; } = rarity ?? new Common();
    [JsonProperty] public RelicPreservation Preservation { get; init; } = preservation ?? new Piece();
    [JsonProperty] public RelicAppraisal Appraisal { get; init; } = appraisal ?? new Unidentified();

    public long DailyGold => (long) (
        Rarity.DailyGoldLimit *
        Preservation.PreservationRate *
        Appraisal.AppraisalRate
    );
}