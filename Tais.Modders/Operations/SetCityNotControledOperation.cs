using Tais.Citys;
using Tais.Modders.Interfaces;

namespace Tais.Modders.Operations;

public class SetCityNotControledOperation : IOperation
{
    public void Do(string desc, object target)
    {
        var city = target as City;
        city.IsOwned = false;
    }

    public override string ToString()
    {
        return $"set city controled flag false";
    }
}
