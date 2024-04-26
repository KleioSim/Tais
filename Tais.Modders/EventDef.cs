using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Modders;

public class EventDef : IEventDef
{
    public IVaildDate VaildDate { get; init; }

    public ICondition TriggerCondition { get; init; }

    public IStringBuilder Title { get; init; }

    public string Desc { get; init; }

    public IOpitionDef Opition { get; init; }
}

public class OpitionDef : IOpitionDef
{
    public string Desc { get; init; }

    public ICommandBuilder CommandBuilder { get; init; }
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
