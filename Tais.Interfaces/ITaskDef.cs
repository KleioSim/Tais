namespace Tais.Interfaces;

public interface ITaskDef
{
    int RequestActionPoint { get; }

    string Name { get; }
    float Speed { get; }

    ICondition Condition { get; }
    IOperation Operation { get; }
}

public interface IOperation
{
    void Do(string desc, object target);
}

public interface ICondition
{
    bool IsSatisfied(object target);
}