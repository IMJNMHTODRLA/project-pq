using Godot;
using ProjectPQ.Scripts.Games;

namespace ProjectPQ.Scripts.Menu;

public partial class MainMenu : Control
{
    [Export] private Button NewGameBtn = null!;

    public override void _Ready()
    {
        NewGameBtn.Pressed += OnNewGame;
    }

    public void OnNewGame()
    {
        GameManager.Self.NewGame();
    }
}