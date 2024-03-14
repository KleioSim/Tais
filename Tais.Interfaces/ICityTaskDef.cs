namespace Tais.Interfaces;

public interface ICityTaskDef
{
    string Name { get; }
    ICondition Condition { get; }
}
