using Tais.Modders.Interfaces;

namespace Tais.Effects;

public class Effect : IEffect
{
    public string Desc { get; set; }

    public float Percent { get; set; }
}

public class PopTaxChangePercentEffect : IEffect
{
    public string Desc { get; init; }

    public float Percent { get; init; }

    public PopTaxChangePercentEffect(string from, float percent)
    {
        Desc = from;
        Percent = percent;
    }
}