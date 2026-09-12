namespace ProjectPQ.Scripts.Games.Relics.Relics;

public abstract partial class Relic(
    RelicRarity rarity,
    RelicPreservation preservation,
    RelicAppraisal appraisal
) : Item
{
}