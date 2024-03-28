using Tais.Citys;
using Tais.Commands;
using Tais.Effects;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.Pops;

namespace Tais.Sessions;

class Session : ISession
{
    public IEvent? CurrEvent { get; private set; }
    public IDate Date => date;

    public IEnumerable<ITask> Tasks => tasks;
    public IFinance Finance => finance;
    public IEnumerable<ICity> Cities => cities;
    public ICentralGov CentralGov => centralGov;
    public IList<IToast> Toasts => toasts;
    public IPlayer Player => player;

    internal Date date = new Date();
    internal Finance finance = new Finance();
    internal List<Task> tasks = new List<Task>();
    internal List<City> cities = new List<City>();
    internal CentralGov centralGov = new CentralGov();
    internal List<IToast> toasts = new List<IToast>();
    internal Player player = new Player();

    public void OnCommand(ICommand command)
    {
        switch (command)
        {
            case Cmd_TaskStart cmd_TaskStart:
                {
                    var task = new Task(cmd_TaskStart.Def as ITaskDef, cmd_TaskStart.Target);
                    tasks.Add(task);
                }
                break;
            case Cmd_TaskCancel cmd_TaskCancel:
                {
                    var task = cmd_TaskCancel.Target as Task;
                    tasks.Remove(task);
                }
                break;
            case Cmd_NextDay:
                {
                    date.DaysInc();

                    foreach (var task in tasks)
                    {
                        task.OnDaysInc(date);
                    }

                    foreach (var city in cities)
                    {
                        city.OnDaysInc(date);
                    }

                    tasks.RemoveAll(x => x.Progress >= 100);
                }
                break;
            case Cmd_ChangeFamilyAttitude cmd_ChangeFamilyAttitude:
                {
                    var pop = cmd_ChangeFamilyAttitude.Target as Pop;

                    var attitude = pop.Family.Attitude as Attitude;

                    attitude.items.Add(new Attitude.Item(cmd_ChangeFamilyAttitude.Reason, cmd_ChangeFamilyAttitude.Value));
                }
                break;
            default:
                throw new Exception($"Not support cmd type {command.GetType()}");
        }
    }

    public Session()
    {
        City.OnOwnerChanged = null;
        City.OnOwnerChanged += (flag, city) =>
        {
            if (flag)
            {
                finance.incomes.Add(city.PopTax);
            }
            else
            {
                finance.incomes.Remove(city.PopTax);
            }
        };

        City.OnBufferAdded = null;
        City.OnBufferAdded += (buff, city) =>
        {
            toasts.Add(new Toast() { Desc = $"Add Buff {buff.GetType().Name} to {city.Name}" });
        };

        City.OnBufferRemoved = null;
        City.OnBufferRemoved += (buff, city) =>
        {
            toasts.Add(new Toast() { Desc = $"Remove Buff {buff.GetType().Name} from {city.Name}" });
        };

        player.CalcUsedEngine = () =>
        {
            return tasks.Sum(x => x.Def.RequestActionPoint);
        };

        finance.spends.Add(centralGov.RequestTax);
    }
}

class Toast : IToast
{
    public string Desc { get; init; }
}


class CentralGov : ICentralGov
{
    public IRequestTax RequestTax => requestTax;

    public float InitTaxValue { get; internal set; }

    public RequestTax requestTax;

    public CentralGov()
    {
        requestTax = new RequestTax(this);
    }
}

class RequestTax : IRequestTax
{
    public float BaseValue => centralGov.InitTaxValue;

    public IEnumerable<IEffect> Effects => effects;

    private List<Effect> effects = new List<Effect>();

    private CentralGov centralGov;

    public RequestTax(CentralGov centralGov)
    {
        this.centralGov = centralGov;
    }
}

class Event : IEvent
{
    public static Action? OnSelected;

    public void OnSelect()
    {
        OnSelected?.Invoke();
    }
}

class Task : ITask
{
    public ITaskDef Def { get; }
    public object Target { get; }

    public float Progress
    {
        get => progress;
        internal set
        {
            progress = value;
            if (Progress >= 100)
            {
                Def.Command.Target = Target;
                Def.Command.Reason = Def.Name;

                CommandSender.Send(Def.Command);
            }
        }
    }

    private float progress;

    public Task(ITaskDef def, object target)
    {
        this.Def = def;
        this.Target = target;
    }

    internal void OnDaysInc(IDate date)
    {
        Progress += Def.Speed;
    }
}

class Finance : IFinance
{
    public float Current { get; set; }

    public float Surplus => Incomes.Sum(x => x.CurrValue) - Spends.Sum(x => x.CurrValue);

    public IEnumerable<IEffectValue> Incomes => incomes;

    public IEnumerable<IEffectValue> Spends => spends;

    public HashSet<IEffectValue> incomes = new HashSet<IEffectValue>();
    public HashSet<IEffectValue> spends = new HashSet<IEffectValue>();

    internal void OnNextTurn()
    {
        Current += Surplus;
    }
}

class Player : IPlayer
{
    internal Func<int> CalcUsedEngine { get; set; }

    public int FreeActionPoints => TotalActionPoints - CalcUsedEngine();

    public int TotalActionPoints { get; private set; }

    internal void Initialize(IPlayerInitData initData)
    {
        TotalActionPoints = 10;
    }
}



public class PopInitData : IPopInitData
{
    public int Count { get; init; }

    public string FamilyName { get; init; }

    public string PopName { get; init; }
}