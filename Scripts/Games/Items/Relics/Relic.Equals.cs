using ProjectPQ.Scripts.Games.Relics;

namespace ProjectPQ.Scripts.Games.Items.Relics;

public abstract partial class Relic
{
    public override bool Equals(Item? other)
    {
        if (other is not Relic relic)
            return false;
        
        return
            base.Equals(relic) &&
            Rarity.Type == relic.Rarity.Type &&
            Preservation.Type == relic.Preservation.Type &&
            Appraisal.Type == relic.Appraisal.Type;
    }

    public bool IsPiece => Preservation.Type == RelicPreservation.Piece.Type;
}