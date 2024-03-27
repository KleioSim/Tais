namespace Tais.Modders.Interfaces;

public interface IEffectValue
{
    float CurrValue => BaseValue * (1 + Effects.Sum(x => x.Percent));
    float BaseValue { get; }

    IEnumerable<IEffect> Effects { get; }
}
