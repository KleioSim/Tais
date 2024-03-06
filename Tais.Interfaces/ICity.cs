namespace Tais.Interfaces;

public interface ICity
{
    IGroupValue PopCount { get; }
    IEffectValue PopTax { get; }
    public bool IsOwned { get; }
}

public interface IGroupValue
{
    float Current { get; }
    IEnumerable<(string desc, float count)> Items { get; }
}