using Tais.Commands;

namespace Tais.Modders.Interfaces;

public interface IEventDef
{
    IVaildDate VaildDate { get; }
    ICondition TriggerCondition { get; }
    ICommand Command { get; }
}

public interface IVaildDate
{
    bool Check(object date);
}