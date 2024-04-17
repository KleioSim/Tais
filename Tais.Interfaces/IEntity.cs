using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface IEntity
{
    string Id { get; }

    IEntityDef Def { get; }
}
