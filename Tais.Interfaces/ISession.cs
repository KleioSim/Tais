using Tais.Commands;

namespace Tais.Interfaces;

public interface ISession
{
    IEvent CurrEvent { get; }

    IFinance Finance { get; }
    IEnumerable<ICity> Cities { get; }
    IEnumerable<ITask> Tasks { get; }

    void OnCommand(ICommand command);
}
