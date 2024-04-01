namespace Tais.Interfaces;

public interface IPlayer
{
    int FreeActionPoints { get; }
    int TotalActionPoints { get; }

    bool IsRevoked { get; }
    bool IsDead { get; }
}