using Tais.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Modders.DataWappers.Visitors;

public class CityMonthPopTax : IDataWapper<float>
{
    public float GetValue(object target)
    {
        var context = (ProcessContext)target;

        if (context.current is IPop pop)
        {
            return pop.City.PopTax.CurrValue;
        }
        else if (context.current is ICity city)
        {
            return city.PopTax.CurrValue;
        }
        else
        {
            throw new Exception();
        }
    }
}