namespace ProjectPQ.Scripts.Games.Items.Relics;

public abstract partial class Relic
{
    public override bool Equals(Item? other)
    {
        if (other is not Relic relic)
            return false;
        
        return
            base.Equals(relic) &&
            Rarity == relic.Rarity &&
            Preservation == relic.Preservation &&
            Appraisal == relic.Appraisal;
    }
}