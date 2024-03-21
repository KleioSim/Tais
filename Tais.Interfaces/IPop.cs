namespace Tais.Interfaces;

public interface IPop
{
    string Name { get; }
    float Count { get; set; }
    bool IsRegisted { get; }

    IFamily Family { get; }

    //IEnumerable<ITaskDef> TaskDefs { get; }
}

public interface IFamily
{
    string Name { get; }
    IGroupValue Attitude { get; }
}
