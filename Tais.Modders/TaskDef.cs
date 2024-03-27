using Tais.Modders.Interfaces;

namespace Tais.Modders;

class TaskDef : ITaskDef
{
    public string Name { get; init; }

    public ICondition Condition { get; init; }

    public float Speed { get; init; }

    public IOperation Operation { get; init; }

    public int RequestActionPoint { get; init; }

    public TaskDef()
    {

    }
}
