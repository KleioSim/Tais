using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Modders.DataWappers.Visitors;

class CentralGovRequestTax : IDataWapper<float>
{
    public float GetValue(object target)
    {
        var context = (ProcessContext)target;
        return context.session.CentralGov.RequestTax.CurrValue;
    }
}
