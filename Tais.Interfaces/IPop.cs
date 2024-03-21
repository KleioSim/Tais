namespace Tais.Interfaces;

public interface IPop
{
    string Name { get; }
    float Count { get; set; }
    bool IsRegisted { get; }

    IFamily Family { get; }

    IEnumerable<ITaskDef> TaskDefs { get; }
}

public interface IFamily
{
    string Name { get; }
    IGroupValue Attitude { get; }
}


public interface IPopDef
{
    string Name { get; }
    bool IsRegisted { get; }
    bool HasFamily { get; }

    IEnumerable<ITaskDef> TaskDefs { get; }

}

public interface IPopInitData
{
    int Count { get; }
    string familyName { get; }
}