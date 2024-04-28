namespace Tais.Modders.Interfaces;

public interface IWarnDef
{
    string Name { get; }
    IStringBuilder Desc { get; }
    ICondition Condition { get; }
}
