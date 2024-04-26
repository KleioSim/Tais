using Tais.Interfaces;

namespace Tais.Modders.DataWappers.Visitors;

public class CityMonthPopTax : IDataWapper<float>
{
    public float GetValue(object target)
    {
        //if (target is IPop pop)
        //{
        //    return pop.City.PopTax.CurrValue;
        //}
        //else if (target is ICity city)
        //{
        //    return city.PopTax.CurrValue;
        //}
        //else
        {
            throw new Exception();
        }
    }
}