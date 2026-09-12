using System.Text.Json.Serialization;

namespace ProjectPQ.Scripts.Games.Relics.Relics.Forests.Commons;

[method: JsonConstructor]
[JsonTypeId(0xCADFBECA52A5D31E)]
public partial class OldPotteryFragments(
    RelicPreservation pre,
    RelicAppraisal app
) : Relic(RelicRarity.Common, pre, app)
{
    public OldPotteryFragments() :
        this(RelicPreservation.GetRandPreservation(), RelicAppraisal.Unidentified)
    {
    }

    protected override string BaseName => Tr("낡은 토기 조각");
    protected override string BaseDescription => "매우 Test한 설명 :>>>>";

    protected override string PreservationFolder => "res://Scenes/Games/Items/Relics/OldPotteryFragments/PreservationIcon/";
}