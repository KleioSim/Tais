using System.Runtime.Intrinsics.X86;
using Tais.Commands;
using Tais.Interfaces;

namespace Tais.Sessions;

class Session : ISession
{
    public IEvent CurrEvent { get; private set; }

    public IEnumerable<ITask> Tasks => tasks;
    public IFinance Finance => finance;
    public IEnumerable<ICity> Cities => cities;

    internal Finance finance = new Finance();
    internal List<Task> tasks = new List<Task>();
    internal List<City> cities = new List<City>();

    public void OnCommand(ICommand command)
    {
        throw new NotImplementedException();
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
    }
}

class Event : IEvent
{
    public static Action OnSelected;

    public void OnSelect()
    {
        OnSelected.Invoke();
    }
}

class Task : ITask
{

}

class City : ICity
{
    public static Action<bool, City> OnOwnerChanged;

    public IEffectValue PopTax => popTax;

    public int PopCount { get; private set; }

    public bool IsOwned
    {
        get => isOwned;
        set
        {
            isOwned = value;
            OnOwnerChanged.Invoke(isOwned, this);
        }
    }

    private PopTax popTax;
    private bool isOwned;

    public City(int popCount, bool isOwned)
    {
        popTax = new PopTax(this);
        PopCount = popCount;

        IsOwned = isOwned;
    }
}

class PopTax : IEffectValue
{
    public float BaseValue => from.PopCount / 10000f;

    public IEnumerable<IEffect> Effects => effects;

    public List<Effect> effects = new List<Effect>();

    public ICity from;

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

    internal void NextTurn()
    {
        Current += Surplus;
    }
}

class EffectValue : IEffectValue
{
    public float BaseValue { get; set; }

    public IEnumerable<IEffect> Effects => effects;

    public List<Effect> effects = new List<Effect>();
}

class Effect : IEffect
{
    public string Desc { get; set; }

    public float Percent { get; set; }
}