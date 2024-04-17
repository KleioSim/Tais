namespace Tais.Modders.Interfaces;

public interface IPopDef : IEntityDef
{
    string PopName { get; }
    bool IsRegisted { get; }
    bool HasFamily { get; }
}
