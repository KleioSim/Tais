using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tais.Interfaces;

namespace Tais.Modders.DataWappers.Visitors;

class CentralGovRequestTax : IDataWapper<float>
{
    public float GetValue(object target)
    {
        var centralGov = target as ICentralGov;
        return centralGov.RequestTax.CurrValue;
    }
}
