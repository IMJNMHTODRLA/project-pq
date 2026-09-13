using Godot;
using ProjectPQ.Scripts.Games.Maps.Forests;

namespace ProjectPQ.Scripts.Games.Maps.Museums;

public partial class Museum : Map, IScene<EmptyArgs>
{
    public static string ScenePath => "res://Scenes/Games/Maps/Museums/Museum.tscn";

    [Export] private Area2D ChangeMapArea = null!;

    public override void _Ready()
    {
        base._Ready();
        ChangeMapArea.BodyEntered += OnChangeMap;
    }

    private void OnChangeMap(Node2D _)
    {
        CallableUtils.NextCall(OnChange);
    }

    private void OnChange()
    {
        SceneLoader.Change<Forest, EmptyArgs>();
    }
}