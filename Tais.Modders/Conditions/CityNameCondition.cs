using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Modders.Conditions;

public class CityNameCondition : ICondition
{
    private string cityName;

    public CityNameCondition(string cityName)
    {
        this.cityName = cityName;
    }

    public bool IsSatisfied(object obj)
    {
        return ((ICity)obj).Name == cityName;
    }

    public override string ToString()
    {
        return $"city name equal {cityName}";
    }
}
