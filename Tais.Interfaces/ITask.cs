using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface ITask
{
    ITaskDef Def { get; }
    IEntity Target { get; }

    float Progress { get; }
}
