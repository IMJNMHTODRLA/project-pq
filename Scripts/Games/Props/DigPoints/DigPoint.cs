using Godot;
using ProjectPQ.Scripts.Games.Maps;

namespace ProjectPQ.Scripts.Games.Props.DigPoints;

public readonly record struct DigPointArgs(
    ExcavationMap RandMap
) : ISceneArgs;

public partial class DigPoint : StaticBody2D, IScene<DigPointArgs>
{
    public static string ScenePath => "res://Scenes/Games/Props/DigPoints/DigPoint.tscn";

    public ExcavationMap _randMap = null!;
    [Export] private Area2D _canDigArea = null!;

    public void SceneInit(DigPointArgs args)
    {
        _randMap = args.RandMap;
    }

    public override void _Ready()
    {
        _canDigArea.InputEvent += DigAreaInputEvent;
    }

    private void DigAreaInputEvent(Node _, InputEvent inputEvent, long __)
    {
        if (!inputEvent.IsActionPressed(InputMap.MouseLeft))
            return;

        _randMap.GetRandRelic();
    }
}