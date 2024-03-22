namespace Tais.Interfaces;

public interface ICity
{
    string Name { get; }
    IGroupValue PopCount { get; }
    IEffectValue PopTax { get; }
    bool IsOwned { get; }
    IEnumerable<IPop> Pops { get; }
    IEnumerable<IBuffer> Buffers { get; }
    IEnumerable<IEffect> Effects { get; }
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
    IEnumerable<ITaskDef> TaskDefs { get; }
    IEnumerable<IBufferDef> BufferDefs { get; }
}

public interface IBuffer
{
    string Name { get; }
    IEnumerable<IEffect> Effects { get; }
}

public interface IBufferDef
{
    string BufferName { get; }

    ICondition ValidCondition { get; }
    ICondition InvalidCondition { get; }

    IEnumerable<IEffect> Effects { get; }
}