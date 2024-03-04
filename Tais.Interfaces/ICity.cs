namespace Tais.Interfaces;

public interface ICity
{
    int PopCount { get; }
    IEffectValue PopTax { get; }
    public bool IsOwned { get; }
}