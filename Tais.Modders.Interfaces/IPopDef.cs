namespace Tais.Modders.Interfaces;

public interface IPopDef
{
    string PopName { get; }
    bool IsRegisted { get; }
    bool HasFamily { get; }

    IEnumerable<ITaskDef> TaskDefs { get; }

}
