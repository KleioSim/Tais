using Tais.Commands;

namespace Tais.Modders.Interfaces;

public interface IEventDef
{
    string Title { get; }
    string Desc { get; }

    IVaildDate VaildDate { get; }
    ICondition TriggerCondition { get; }
    IOpitionDef Opition { get; }
    ICommand Command { get; }
}

public interface IVaildDate
{
    bool Check(object date);
}

public interface IOpitionDef
{
    string Desc { get; }
    ICommand Command { get; }
}