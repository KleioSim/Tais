using Tais.Commands;

namespace Tais.Modders.Interfaces;

public interface IEventDef
{
    string Title { get; }
    string Desc { get; }

    IVaildDate VaildDate { get; }
    ICondition TriggerCondition { get; }
    ICommand Command { get; }
}

public interface IVaildDate
{
    bool Check(object date);
}