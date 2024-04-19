using Tais.Commands;

namespace Tais.Modders.Interfaces;

public interface IModder
{
    Dictionary<string, IPopDef> PopDefs { get; }

    ICityDef CityDef { get; }

    IPlayerDef PlayerDef { get; }
    ICentralGovDef CentralGovDef { get; }
}
