namespace Tais.Modders.Interfaces;

public interface IEffect
{
    string Desc { get; }
    float Percent { get; }
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

public class CentralRequestTaxEffect : IEffect
{
    public string Desc { get; init; }

    public float Percent { get; init; }

    public CentralRequestTaxEffect(string from, float percent)
    {
        Desc = from;
        Percent = percent;
    }
}