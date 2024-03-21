using Tais.Commands;

namespace Tais.Interfaces;

public interface ISession
{
    IDate Date { get; }

    IEvent CurrEvent { get; }

    IFinance Finance { get; }
    IEnumerable<ICity> Cities { get; }
    IEnumerable<ITask> Tasks { get; }

    ICentralGov CentralGov { get; }

    void OnCommand(ICommand command);
}