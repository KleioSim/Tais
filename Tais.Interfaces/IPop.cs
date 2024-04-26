using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface IPop : IEntity
{
    string Name { get; }
    float Count { get; set; }
    bool IsRegisted { get; }

    IFamily Family { get; }
    IGroupValue Living { get; }

    IEnumerable<ITaskDef> TaskDefs { get; }
    ICity City { get; }
}

public interface IFamily
{
    string Name { get; }
    IGroupValue Attitude { get; }
}