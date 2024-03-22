namespace Tais.Interfaces;

public interface ICity
{
    string Name { get; }
    IGroupValue PopCount { get; }
    IEffectValue PopTax { get; }
    bool IsOwned { get; }
    IEnumerable<IPop> Pops { get; }

    IEnumerable<ITaskDef> TaskDefs { get; }
}

public interface IGroupValue
{
    float Current { get; }
    IEnumerable<(string desc, float count)> Items { get; }
}

public interface ICityInitData
{
    string CityName { get; }
    bool IsControlled { get; }
}

public interface ICityDef
{
    public IEnumerable<ITaskDef> TaskDefs { get; }
}