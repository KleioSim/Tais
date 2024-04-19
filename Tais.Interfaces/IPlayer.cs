namespace Tais.Interfaces;

public interface IPlayer : IEntity
{
    int FreeActionPoints { get; }
    int TotalActionPoints { get; }

    bool IsRevoked { get; }
    bool IsDead { get; }
}