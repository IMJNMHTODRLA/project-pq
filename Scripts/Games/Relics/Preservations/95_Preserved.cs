using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games.Relics.Preservations;

[JsonTypeId(0x680DB1163BBD4A09)]
public class Preserved : RelicPreservation
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => LangKey.RELIC_PRESERVATION_PRESERVED_NAME.Translate();

    public override float Percent => 0.20f;
    public override double Rate => 0.80;
}