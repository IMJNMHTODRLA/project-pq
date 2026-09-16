using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games.Relics.Preservations;

[JsonTypeId(0x65D00DAA582E6BC6)]
public class Damaged : RelicPreservation
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => LangKey.RELIC_PRESERVATION_DAMAGED_NAME.Translate();

    public override float Percent => 0.40f;
    public override double Rate => 0.40;
}