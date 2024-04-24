using Tais.Modders.Interfaces;

namespace Tais.Modders;

public class PopDef : EntityDef, IPopDef
{
    public string PopName { get; init; }

    public bool IsRegisted { get; init; }

    public bool HasFamily { get; init; }

    public bool HasLiving { get; init; }
}
