using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;
using Tais.Commands;
using Tais.Interfaces;

namespace Tais.Sessions;

class Session : ISession
{
    public IEvent? CurrEvent { get; private set; }

    public IEnumerable<ITask> Tasks => tasks;
    public IFinance Finance => finance;
    public IEnumerable<ICity> Cities => cities;
    public ICentralGov CentralGov => centralGov;

    public IEnumerable<ICityTaskDef> CityTaskDefs => cityTaskDefs;

    internal Finance finance = new Finance();
    internal List<Task> tasks = new List<Task>();
    internal List<City> cities = new List<City>();
    internal CentralGov centralGov = new CentralGov();

    internal List<CityTaskDef> cityTaskDefs = new List<CityTaskDef>();


    public void OnCommand(ICommand command)
    {
        switch (command)
        {
            case Cmd_NextTurn:
                finance.OnNextTurn();
                break;
            case Cmd_TaskStart cmd_TaskStart:
                {
                    var task = new Task(cmd_TaskStart.Def as ICityTaskDef, cmd_TaskStart.Target);
                    tasks.Add(task);
                }
                break;
            case Cmd_TaskCancel cmd_TaskCancel:
                {
                    var task = cmd_TaskCancel.task as Task;
                    tasks.Remove(task);
                }
                break;
            default:
                throw new Exception($"Not support cmd type {command.GetType()}");
        }
    }

    public Session()
    {
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

        finance.spends.Add(centralGov.RequestTax);
    }
}

class CityTaskDef : ICityTaskDef
{
    public string Name { get; internal set; }

    public ICondition Condition { get; internal set; }

    public CityTaskDef(string name, ICondition condition)
    {
        this.Name = name;
        this.Condition = condition;
    }
}

public class Condition : ICondition
{
    public bool IsSatisfied(ICity city)
    {
        return city.Name == "CITY_0";
    }
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
    public ICityTaskDef Def { get; }
    public object Target { get; }

    public Task(ICityTaskDef def, object target)
    {
        this.Def = def;
        this.Target = target;
    }
}

class City : ICity
{
    public static Action<bool, City>? OnOwnerChanged;

    public string Name { get; }
    public IEffectValue PopTax => popTax;
    public IGroupValue PopCount => popCount;
    public IEnumerable<IPop> Pops => pops;

    public bool IsOwned
    {
        get => isOwned;
        set
        {
            isOwned = value;
            OnOwnerChanged?.Invoke(isOwned, this);
        }
    }

    private PopTax popTax;
    private GroupValue popCount;

    private bool isOwned;
    private List<Pop> pops = new List<Pop>();

    public City(string name, bool isOwned, IEnumerable<Pop> pops)
    {
        Name = name;
        popTax = new PopTax(this);
        popCount = new GroupValue();
        popCount.Items = pops.Where(x => x.IsRegisted).Select(x => (x.Name, x.Count));

        this.pops.AddRange(pops);

        IsOwned = isOwned;
    }
}

class GroupValue : IGroupValue
{
    public float Current => Items.Sum(x => x.count);

    public IEnumerable<(string desc, float count)> Items { get; set; }
}

class Pop : IPop
{
    public string Name { get; }
    public float Count { get; set; }
    public bool IsRegisted { get; }

    public Pop(string name, float count, bool isRegisted)
    {
        Name = name;
        Count = count;
        IsRegisted = isRegisted;
    }
}

class PopTax : IEffectValue
{
    public float BaseValue => from.PopCount.Current / 10000f;

    public IEnumerable<IEffect> Effects => effects;

    private List<Effect> effects = new List<Effect>();

    private ICity from;

    public PopTax(ICity city)
    {
        this.from = city;
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

class Effect : IEffect
{
    public string Desc { get; set; }

    public float Percent { get; set; }
}