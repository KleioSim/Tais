using Tais.Interfaces;

namespace Tais.CentralGovs;

class CentralGov : ICentralGov
{
    public IRequestTax RequestTax => requestTax;

    public float InitTaxValue { get; internal set; }

    public RequestTax requestTax;

    public CentralGov()
    {
        requestTax = new RequestTax(this);
    }

    internal IEnumerable<IEvent> OnDaysInc(IDate date)
    {
        var eventObj = new Event();
        yield return eventObj;


        eventObj = new Event();
        yield return eventObj;
    }
}

public class Event : IEvent
{
    public Action OnSelected;

    public void OnSelect()
    {
        OnSelected?.Invoke();
    }
}
