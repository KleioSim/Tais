using Tais.Commands;
using Tais.Modders.Interfaces;

namespace Tais.Modders;

class TaskDef : ITaskDef
{
    public string Name { get; init; }

    public ICondition Condition { get; init; }

    public float Speed { get; init; }

    public int RequestActionPoint { get; init; }

    public IEnumerable<ICommand> Commands { get; init; }

    public TaskDef()
    {

    }
}
