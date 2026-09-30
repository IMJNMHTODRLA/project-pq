using Godot;
using ProjectPQ.Scripts.Games.Items.Relics;
using ProjectPQ.Scripts.Games.Managers;
using ProjectPQ.Scripts.Games.Maps;

namespace ProjectPQ.Scripts.Games.Props.DigPoints;

public readonly record struct DigPointArgs(
    ExcavationMap RandMap
) : ISceneArgs;

public partial class DigPoint : StaticBody2D, IScene<DigPointArgs>
{
    public delegate void OnExcavatedListener(DigPoint dig);
    public event OnExcavatedListener? OnExcavated;

    public static string ScenePath => "res://Scenes/Games/Props/DigPoints/DigPoint.tscn";

    public ExcavationMap _randMap = null!;
    [Export] private Area2D _canDigArea = null!;

    public void SceneInit(DigPointArgs args)
    {
        _randMap = args.RandMap;
    }

    private int _holdTicks = 0;
    private bool _isHolding = false;

    public override void _Ready()
    {
        _canDigArea.InputEvent += HoldingInputEvent;
    }

    private void HoldingInputEvent(Node _, InputEvent inputEvent, long __)
    {
        if (!inputEvent.IsActionPressed(InputMap.MouseLeft))
            return;

        _isHolding = inputEvent.IsPressed();
        if (!_isHolding) _holdTicks = 0;
    }

    public override void _PhysicsProcess(double delta)
    {
        if (!_isHolding)
            return;
        
        if (++_holdTicks >= 5.0.Sec2Tick())
            OnDig();
    }

    private void OnDig()
    {
        _isHolding = false;
        _holdTicks = 0;

        Relic? selectRelic = _randMap.GetRandRelic();
        if (selectRelic == null) return;

        PlayerManager.Self.Player.AddItem(selectRelic);

        OnExcavated?.Invoke(this);
    }
}