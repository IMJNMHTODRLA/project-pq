namespace ProjectPQ.Scripts.Games.Relics.Relics;

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
}