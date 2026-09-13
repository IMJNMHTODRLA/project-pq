namespace ProjectPQ.Scripts.Games.Entities.Players;

public partial class Player :
    LivingEntity,
    IScene<EmptyArgs>
{
    public Player() : base(maxHealth: 100, health: 100)
    {
        InitializeMove();
    }

    public static string ScenePath => "res://Scenes/Games/Entities/Players/Player.tscn";

    public override string EntityName { get; set; } = "플레이어";

    public override void ReadyProcess()
    {
    }

    public override void TickProcess(double delta)
    {
        ProcessMovement();
    }
}