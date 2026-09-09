namespace ProjectPQ.Scripts.Games.Entities.Players;

public partial class Player
{
    public const long DEFAULT_GOLD = 0L;
    public long Gold { get; set; } = 0L;

    /// <summary>
    /// 마이너스로 들어오면 무조건 false 리턴
    /// </summary>
    /// <param name="amount">마이너스 넣지 마셈ㅇㅇ 넣으면 화낼거야</param>
    /// <returns>true or false</returns>
    public bool CanAffordGold(long amount) =>
        amount >= 0 && Gold >= amount;
    
    public bool TrySpendGold(long amount)
    {
        bool canAfford = CanAffordGold(amount);
        if (canAfford) Gold -= amount;
        return canAfford;
    }
}