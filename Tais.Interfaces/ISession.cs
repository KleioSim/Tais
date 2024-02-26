using Tais.Commands;

namespace Tais.Interfaces;

public interface ISession
{
    IEnumerable<ITask> Tasks { get; }

    IEvent CurrEvent { get; }

    void OnCommand(ICommand command);
}
