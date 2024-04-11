using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Modders.Conditions;

public class CityNameCondition : ICondition
{
    private string cityName;

    public CityNameCondition(string cityName)
    {
        this.cityName = cityName;
    }

    public bool IsSatisfied(IProcessContext context)
    {
        if (((ProcessContext)context).current is not ICity city)
        {
            throw new Exception();
        }

        return city.Name == cityName;
    }

    public override string ToString()
    {
        return $"city name equal {cityName}";
    }
}

//public class IConditionContext
//{
//    ISession GetSession();
//    T GetCurrent<T>() where T : IEntity;
//}