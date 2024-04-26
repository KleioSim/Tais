namespace Tais.Modders.Interfaces;

public interface IWarnDef
{
    string Name { get; }
    ICondition Condition { get; }
}
