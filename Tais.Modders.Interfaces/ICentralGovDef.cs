namespace Tais.Modders.Interfaces;

public interface ICentralGovDef
{
    IEnumerable<IEventDef> EventDefs { get; }
}