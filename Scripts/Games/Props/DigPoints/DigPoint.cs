using Godot;

namespace ProjectPQ.Scripts.Games.Props.DigPoints;

public partial class DigPoint : StaticBody2D
{
    [Export] private Area2D CanDigArea = null!;

    public override void _Ready()
    {
        CanDigArea.InputEvent += DigAreaInputEvent;
    }

    private void DigAreaInputEvent(Node _, InputEvent inputEvent, long __)
    {
        if (!inputEvent.IsActionPressed(InputMap.MouseLeft))
            return;

        GD.Print("DigArea action");
    }
}