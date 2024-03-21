namespace Tais.Interfaces;

public interface ITaskDef
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

public interface ICondition
{
    bool IsSatisfied(object target);
}