using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.ProcessContexts;

public struct ProcessContext : IProcessContext
{
    public IEntity current { get; init; }
    public ISession session { get; init; }
}