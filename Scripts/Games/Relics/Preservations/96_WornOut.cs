using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games.Relics.Preservations;

[JsonTypeId(0x78DADE29CDC43F14)]
public class WornOut : RelicPreservation
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => LangKey.RELIC_PRESERVATION_WORN_OUT_NAME.Translate();

    public override float Percent => 0.30f;
    public override double Rate => 0.60;
}