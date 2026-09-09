namespace ProjectPQ.Scripts.Games.Items.Relics;

public abstract partial class Relic(
    RelicRarity rarity,
    RelicPreservation preservation,
    RelicAppraisal appraisal
) : Item
{
}