using Tais.Buffers;
using Tais.CentralGovs;
using Tais.Citys;
using Tais.Commands;
using Tais.Entities;
using Tais.Events;
using Tais.InitialDatas.Interfaces;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.Pops;
using Tais.Warns;

namespace Tais.Sessions;

class Session : ISession
{
    public IEvent? CurrEvent { get; private set; }
    public IDate Date => date;

    public IEnumerable<ITask> Tasks => tasks;
    public IFinance Finance => finance;
    public IList<IToast> Toasts => toasts;
    public IEnumerable<IWarn> Warns => warnManager;

    public IEnumerable<IEntity> Entities => entityManager;
    public IEnumerable<ICity> Cities => entityManager.OfType<ICity>();
    public ICentralGov CentralGov => entityManager.OfType<ICentralGov>().Single();
    public IPlayer Player => entityManager.OfType<IPlayer>().Single();

    internal Date date = new Date();
    internal Finance finance = new Finance();
    internal List<Task> tasks = new List<Task>();
    internal List<IToast> toasts = new List<IToast>();

    internal EntityManager entityManager;

    private WarnManager warnManager;
    private EventManager eventManager;
    private BufferManager bufferManager;

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
            case Cmd_ChangeFamilyAttitude cmd_ChangeFamilyAttitude:
                {
                    var pop = cmd_ChangeFamilyAttitude.Target as Pop;

                    var attitude = pop.Family.Attitude as Attitude;

                    attitude.items.Add(new Attitude.Item(cmd_ChangeFamilyAttitude.Reason, cmd_ChangeFamilyAttitude.Value));
                }
                break;
            case Cmd_ChangePopCount cmd_ChangePopCount:
                {
                    var pop = cmd_ChangePopCount.Target as Pop;
                    pop.Count += cmd_ChangePopCount.Value;
                }
                break;
            case Cmd_ChangeCityIsControlFlag cmd_ChangeCityIsControlFlag:
                {
                    var city = cmd_ChangeCityIsControlFlag.Target as City;
                    city.IsOwned = cmd_ChangeCityIsControlFlag.Value;
                }
                break;
            case Cmd_RevokePlayerTitle cmd_RevokePlayerTitle:
                {
                    var player = Player as Player;
                    player.IsRevoked = true;
                }
                break;
            case Cmd_ChangeCentralGovTaxLevel cmd_ChangeCentralGovTaxLevel:
                {
                    var centralGov = CentralGov as CentralGov;
                    centralGov.TaxLevel = Enum.Parse<TaxLevel>(cmd_ChangeCentralGovTaxLevel.Value);
                }
                break;
            default:
                throw new Exception($"Not support cmd type {command.GetType()}");
        }
    }

    public IEnumerable<IEvent> OnDaysInc()
    {
        date.DaysInc();

        foreach (var task in tasks)
        {
            task.OnDaysInc(date);
        }

        foreach (City city in Cities)
        {
            city.OnDaysInc(date);
        }

        tasks.RemoveAll(x => x.Progress >= 100);

        finance.OnDaysInc(date);

        bufferManager.OnDaysInc();

        foreach (var @event in eventManager.OnDaysInc())
        {
            yield return @event;
        }
    }

    public Session()
    {
        City.OnOwnerChanged = (flag, city) =>
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

        City.GetPops = (cityName) => Entities.OfType<IPop>().Where(x => x.City == cityName);


        Entity.OnBufferAdded = (buff, target) =>
        {
            toasts.Add(new Toast() { Desc = $"Add Buff {buff.GetType().Name} to {target}" });
        };

        Entity.OnBufferRemoved = (buff, target) =>
        {
            toasts.Add(new Toast() { Desc = $"Remove Buff {buff.GetType().Name} from {target}" });
        };

        Tais.Sessions.Player.CalcUsedEngine = () =>
        {
            return tasks.Sum(x => x.Def.RequestActionPoint);
        };

        entityManager = new EntityManager();
        eventManager = new EventManager(this);
        warnManager = new WarnManager(this);
        bufferManager = new BufferManager(this);
    }
}

class Toast : IToast
{
    public string Desc { get; init; }
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
                Def.Command.Reason = Def.Name;

                if (Def.Command is ICommandWithTarget commandWithTarget)
                {
                    commandWithTarget.Target = Target;
                }

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
    public static Func<IDate> GetDate;

    public float ExpectYearReserve => Current + Surplus * (12 - currentYearIncomes.Count);
    public float Current { get; set; }

    public float Surplus => Incomes.Sum(x => x.CurrValue) - Spends.Sum(x => x.CurrValue);

    public IEnumerable<IEffectValue> Incomes => incomes;

    public IEnumerable<IEffectValue> Spends => spends;

    public HashSet<IEffectValue> incomes = new HashSet<IEffectValue>();
    public HashSet<IEffectValue> spends = new HashSet<IEffectValue>();

    private Dictionary<int, float> currentYearIncomes = new Dictionary<int, float>();

    internal void OnDaysInc(IDate date)
    {
        if (date.Month == 1 && date.Day == 1)
        {
            currentYearIncomes.Clear();
        }

        if (date.Day == 30)
        {
            currentYearIncomes.Add(date.Month, Surplus);
            Current += Surplus;
        }
    }
}

class Player : Entity<IPlayerDef>, IPlayer
{
    internal static Func<int> CalcUsedEngine { get; set; }

    public int FreeActionPoints => TotalActionPoints - CalcUsedEngine();

    public int TotalActionPoints { get; private set; }

    public bool IsRevoked { get; internal set; }

    public bool IsDead { get; internal set; }

    public Player(string id, IPlayerDef def, IPlayerInitData playerInitData) : base(id, def)
    {
        TotalActionPoints = 10;
    }
}

//public class PlayerTitleRevokedEvent : IEvent
//{
//    public string Title => throw new NotImplementedException();

//    public string Desc => throw new NotImplementedException();

//    public void OnSelect()
//    {
//        throw new NotImplementedException();
//    }
//}

//public class PlayerDeadEvent : IEvent
//{
//    public string Title => throw new NotImplementedException();

//    public string Desc => throw new NotImplementedException();

//    public void OnSelect()
//    {
//        throw new NotImplementedException();
//    }
//}