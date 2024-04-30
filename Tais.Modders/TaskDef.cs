using Tais.Commands;
using Tais.Modders.Interfaces;

namespace Tais.Modders;

public class TaskDef : ITaskDef
{
    public string Name { get; init; }

    public ICondition Condition { get; init; }

    public float Speed { get; init; }

    public int RequestActionPoint { get; init; }

    public IEnumerable<ICommandBuilder> CommandBuilders { get; init; }

    public TaskDef()
    {

    }
}
