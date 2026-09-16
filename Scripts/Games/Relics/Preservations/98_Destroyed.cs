using ProjectPQ.addons.localizers.Maps;

namespace ProjectPQ.Scripts.Games.Relics.Preservations;

[JsonTypeId(0x6CC3E218E9E2CB8E)]
public class Destroyed : RelicPreservation
{
    protected override string Color => ColorUtils.White;
    protected override string BaseName => LangKey.RELIC_PRESERVATION_DESTROYED_NAME.Translate();

    public override float Percent => 0.50f;
    public override double Rate => 0.20;
}