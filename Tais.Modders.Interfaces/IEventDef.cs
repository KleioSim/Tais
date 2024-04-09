using Tais.Commands;

namespace Tais.Modders.Interfaces;

public interface IEventDef
{
    IVaildDate VaildDate { get; }
    bool isTrigger(object obj);
    ICommand Command { get; }
}

public interface IVaildDate
{
    bool Check(object date);
}