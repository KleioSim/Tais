namespace Tais.Interfaces;

public interface ICityTaskDef
{
    string Name { get; }
    float Speed { get; }

    ICondition Condition { get; }
    IOperation Operation { get; }
}

public interface IOperation
{
    void Do(object target);
}