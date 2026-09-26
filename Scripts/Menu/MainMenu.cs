using Godot;
using ProjectPQ.Scripts.Games;
using static ProjectPQ.Scripts.Games.GameSessionManager;

namespace ProjectPQ.Scripts.Menu;

public partial class MainMenu : Control
{
    [Export] private Button NewGameBtn = null!;

    public override void _Ready()
    {
        NewGameBtn.Pressed += NewGame;
    }

    private async void NewGame()
    {
        SessionResult result = await GameSessionManager.Self.NewSession();

        GD.Print(result.ToString());
    }
}