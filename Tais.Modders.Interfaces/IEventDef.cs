using Tais.Commands;

namespace Tais.Modders.Interfaces;

public interface IEventDef
{
    IStringBuilder Title { get; }
    string Desc { get; }

    IVaildDate VaildDate { get; }
    ICondition TriggerCondition { get; }
    IOpitionDef Opition { get; }
}

public interface IVaildDate
{
    bool Check(object date);
}

public interface IOpitionDef
{
    string Desc { get; }
    ICommandBuilder CommandBuilder { get; }
}
