using Microsoft.VisualBasic;
using Tais.Entities;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.CentralGovs;

class CentralGov : Entity, ICentralGov
{
    public IRequestTax RequestTax => requestTax;

    public float InitTaxValue { get; internal set; }

    public RequestTax requestTax;

    public ICentralGovDef def { get; set; }

    public CentralGov()
    {
        requestTax = new RequestTax(this);
    }

    internal void OnDaysInc(IDate date)
    {

    }

    internal void Initialize(ICentralGovDef def)
    {
        this.def = def;
    }
}