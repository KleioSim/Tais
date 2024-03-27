using Tais.Modders.Interfaces;

namespace Tais.Modders;

class Modder : IModder
{
    public Dictionary<string, IPopDef> PopDefs { get; init; }

    public ICityDef CityDef { get; init; }
}
