using Tais.Modders.Interfaces;

namespace Tais.Modders;

internal class CentralGovDef : ICentralGovDef
{
    public IEnumerable<IEventDef> EventDefs { get; init; }
}