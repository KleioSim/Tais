using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.CentralGovs;

class CentralGov : ICentralGov
{
    public IRequestTax RequestTax => requestTax;

    public float InitTaxValue { get; internal set; }

    public RequestTax requestTax;

    private IEnumerable<IEventDef> eventDefs = new IEventDef[]
    {
        new EventDef() { VaildDate = new VaildDate() { Day = 1 } },
        new EventDef() { VaildDate = new VaildDate() { Day = 1, Month = 1} },
    };

    public CentralGov()
    {
        requestTax = new RequestTax(this);
    }

    internal IEnumerable<IEventDef> OnDaysInc(IDate date)
    {
        foreach (var def in eventDefs)
        {
            if (!def.VaildDate.Check(date))
            {
                continue;
            }

            if (def.isTrigger(this))
            {
                yield return def;
            }
        }
    }
}

internal class EventDef : IEventDef
{
    public IVaildDate VaildDate { get; set; }

    public bool isTrigger(object obj)
    {
        return true;
    }
}

public class VaildDate : IVaildDate
{
    public int? Year { get; init; }
    public int? Month { get; init; }
    public int? Day { get; init; }

    public bool Check(object target)
    {
        var date = (IDate)target;

        if (Year != null && Year != date.Year)
        {
            return false;
        }
        if (Month != null && Month != date.Month)
        {
            return false;
        }
        if (Day != null && Day != date.Day)
        {
            return false;
        }

        return true;
    }
}

