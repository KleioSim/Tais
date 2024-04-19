using Tais.Commands;

namespace Tais.Interfaces;

public interface ISession
{
    IPlayer Player { get; }
    IDate Date { get; }

    IEvent CurrEvent { get; }

    IFinance Finance { get; }
    IEnumerable<ITask> Tasks { get; }
    IList<IToast> Toasts { get; }
    IEnumerable<IWarn> Warns { get; }

    IEnumerable<IEntity> Entities { get; }
    IEnumerable<ICity> Cities { get; }
    ICentralGov CentralGov { get; }

    void OnCommand(ICommand command);
    IEnumerable<IEvent> OnDaysInc();
}