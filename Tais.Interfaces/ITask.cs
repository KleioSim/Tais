using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface ITask
{
    ITaskDef Def { get; }
    object Target { get; }

    float Progress { get; }
}
