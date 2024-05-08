using Tais.Modders.Interfaces;

namespace Tais.Modders;

public class WarnDef : IWarnDef
{
    public string Name { get; init; }
    public IStringBuilder Desc { get; init; }
    public ICondition Condition { get; init; }
}

public class WarnDef<T> : WarnDef
    where T : EntityDef
{

}