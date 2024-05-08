using Tais.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Modders.DataWappers.Visitors;

public class ExpectFinanceYearReserve : IDataWapper<float>
{
    public float GetValue(object target)
    {
        var context = (ProcessContext)target;

        return context.session.Finance.ExpectYearReserve;
    }
}