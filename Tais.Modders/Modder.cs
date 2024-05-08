using Tais.Commands;
using Tais.Modders.Interfaces;

namespace Tais.Modders;

class Modder : IModder
{
    public Dictionary<string, IPopDef> PopDefs { get; init; }

    public ICityDef CityDef { get; init; }

    public ICentralGovDef CentralGovDef { get; init; }

    public IPlayerDef PlayerDef { get; init; }

    internal IEnumerable<EntityDef> EntityDefs => PopDefs.Values.OfType<EntityDef>()
        .Append(CityDef as EntityDef)
        .Append(CentralGovDef as EntityDef)
        .Append(PlayerDef as EntityDef)
        .Where(x => x != null);
}