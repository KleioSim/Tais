namespace Tais.Modders.Interfaces;

public interface IEventDef
{
    IVaildDate VaildDate { get; }
    bool isTrigger(object obj);
}

public interface IVaildDate
{
    bool Check(object date);
}