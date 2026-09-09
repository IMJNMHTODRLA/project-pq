using System.Text.Json.Serialization;
using ProjectPQ.Scripts.Games.Items.Relics.Appraisals;
using ProjectPQ.Scripts.Games.Items.Relics.Rarities;

namespace ProjectPQ.Scripts.Games.Items.Relics.Forests.Commons;

[method: JsonConstructor]
public partial class OldPotteryFragments(
    RelicRarity rarity,
    RelicPreservation preservation,
    RelicAppraisal appraisal
) : Relic(rarity, preservation, appraisal)
{
    public OldPotteryFragments(RelicPreservation pre) :
        this(new Common(), pre, new Unidentified())
    {
    }

    protected override string BaseName => Tr("낡은 토기 조각");
    protected override string BaseDescription => "매우 Test한 설명 :>>>>";

    protected override string PreservationFolder { get; } = "res://Scenes/Games/Items/Relics/OldPotteryFragments/PreservationIcon/";
}