using Tais.Commands;

namespace Tais.Modders.Interfaces;

public interface ITaskDef
{
    int RequestActionPoint { get; }

    string Name { get; }
    float Speed { get; }

    ICondition Condition { get; }
    ICommand Command { get; }
}
