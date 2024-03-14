namespace Tais.Interfaces;

public interface ITask
{
    ICityTaskDef Def { get; }
    object Target { get; }

    float Progress { get; }
}
