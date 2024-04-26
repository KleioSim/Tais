using Tais.Modders.Interfaces;

namespace Tais.Modders;

public class WarnDef : IWarnDef
{
    public string Name { get; init; }
    public ICondition Condition { get; init; }
}