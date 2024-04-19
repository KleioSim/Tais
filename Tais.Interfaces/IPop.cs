using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface IPop : IEntity
{
    string Name { get; }
    float Count { get; set; }
    bool IsRegisted { get; }

    IFamily Family { get; }

    IEnumerable<ITaskDef> TaskDefs { get; }
    string City { get; }
}

public interface IFamily
{
    string Name { get; }
    IGroupValue Attitude { get; }
}