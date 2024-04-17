using Tais.Commands;

namespace Tais.Interfaces;

public interface ISession
{
    IPlayer Player { get; }
    IDate Date { get; }

    IEvent CurrEvent { get; }

    IFinance Finance { get; }
    IEnumerable<ICity> Cities { get; }
    IEnumerable<ITask> Tasks { get; }
    IList<IToast> Toasts { get; }

    ICentralGov CentralGov { get; }
    IEnumerable<IWarn> Warns { get; }

    void OnCommand(ICommand command);
    IEnumerable<IEvent> OnDaysInc();
}