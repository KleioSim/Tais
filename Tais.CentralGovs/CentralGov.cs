using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.CentralGovs;

class CentralGov : ICentralGov
{
    public IRequestTax RequestTax => requestTax;

    public float InitTaxValue { get; internal set; }

    public RequestTax requestTax;

    private ICentralGovDef def;

    public CentralGov()
    {
        requestTax = new RequestTax(this);
    }

    internal IEnumerable<IEventDef> OnDaysInc(IDate date)
    {
        foreach (var eventDef in def.EventDefs)
        {
            if (!eventDef.VaildDate.Check(date))
            {
                continue;
            }

            if (eventDef.isTrigger(this))
            {
                yield return eventDef;
            }
        }
    }

    internal void Initialize(ICentralGovDef def)
    {
        this.def = def;
    }
}


