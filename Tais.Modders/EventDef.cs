using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Modders;

public class EventDef : IEventDef
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
