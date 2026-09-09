#if TOOLS
using Godot;

namespace ProjectPQ.addons.localizers;

[Tool]
public partial class Localizers : EditorPlugin
{
    public override void _EnterTree() =>
        AddToolMenuItem(
            "Generate Localization",
            Callable.From(GenerateLocalization)
        );

    public override void _ExitTree() =>
        RemoveToolMenuItem(
            "Generate Localization"
        );

    private void GenerateLocalization()
    {
        LocalizationGenerator.Generate();
        GD.Print("Localization generated!");
    }
}
#endif
