using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface ICity : IEntity
{
    string Name { get; }
    IGroupValue PopCount { get; }
    IEffectValue PopTax { get; }
    bool IsOwned { get; }
    IEnumerable<IPop> Pops { get; }
    IEnumerable<IBuffer> Buffers { get; }
    IEnumerable<IEffect> Effects { get; }
}

public interface IGroupValue
{
    float Current { get; }
    IEnumerable<(string desc, float count)> Items { get; }
}

public interface IBuffer
{
    string Name { get; }
    IEnumerable<IEffect> Effects { get; }
}