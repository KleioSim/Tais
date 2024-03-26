﻿using System;
using Tais.Commands;
using Tais.Interfaces;

namespace Tais.Sessions;

class Session : ISession
{
    public IEvent? CurrEvent { get; private set; }
    public IDate Date => date;

    public IEnumerable<ITask> Tasks => tasks;
    public IFinance Finance => finance;
    public IEnumerable<ICity> Cities => cities;
    public ICentralGov CentralGov => centralGov;
    public IEnumerable<IToast> Toasts => toasts;

    internal Date date = new Date();
    internal Finance finance = new Finance();
    internal List<Task> tasks = new List<Task>();
    internal List<City> cities = new List<City>();
    internal CentralGov centralGov = new CentralGov();
    internal List<Toast> toasts = new List<Toast>();

    public void OnCommand(ICommand command)
    {
        switch (command)
        {
            case Cmd_NextTurn:
                toasts.Clear();
                //finance.OnNextTurn();
                break;
            case Cmd_TaskStart cmd_TaskStart:
                {
                    var task = new Task(cmd_TaskStart.Def as ITaskDef, cmd_TaskStart.Target);
                    tasks.Add(task);
                }
                break;
            case Cmd_TaskCancel cmd_TaskCancel:
                {
                    var task = cmd_TaskCancel.task as Task;
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

        City.OnBufferAdded += (buff, city) =>
        {
            toasts.Add(new Toast() { Desc = $"Add Buff {buff.GetType().Name} to {city.Name}" });
        };

        City.OnBufferRemoved += (buff, city) =>
        {
            toasts.Add(new Toast() { Desc = $"Remove Buff {buff.GetType().Name} from {city.Name}" });
        };

        finance.spends.Add(centralGov.RequestTax);
    }
}

class Toast : IToast
{
    public string Desc { get; init; }
}

class TaskDef : ITaskDef
{
    public string Name { get; }

    public ICondition Condition { get; }

    public float Speed { get; }

    public IOperation Operation { get; }

    public TaskDef(string name, float speed, ICondition condition, IOperation operation)
    {
        this.Name = name;
        this.Condition = condition;
        this.Speed = speed;
        this.Operation = operation;
    }
}

public class SetCityNotControledOperation : IOperation
{
    public void Do(string desc, object target)
    {
        var city = target as City;
        city.IsOwned = false;
    }

    public override string ToString()
    {
        return $"set city controled flag false";
    }
}

public class CityNameCondition : ICondition
{
    private string cityName;

    public CityNameCondition(string cityName)
    {
        this.cityName = cityName;
    }

    public bool IsSatisfied(object obj)
    {
        return ((ICity)obj).Name == cityName;
    }

    public override string ToString()
    {
        return $"city name equal {cityName}";
    }
}

public class FamilyAttitudeMin : ICondition
{
    private float minValue;

    public FamilyAttitudeMin(float minValue)
    {
        this.minValue = minValue;
    }

    public bool IsSatisfied(object target)
    {
        var city = target as ICity;
        var pop = city.Pops.Single(x => x.Family != null);

        return pop.Family.Attitude.Current > minValue;
    }
}

public class FamilyAttitudeMax : ICondition
{
    private float maxValue;

    public FamilyAttitudeMax(float maxValue)
    {
        this.maxValue = maxValue;
    }

    public bool IsSatisfied(object target)
    {
        var city = target as ICity;
        var pop = city.Pops.Single(x => x.Family != null);

        return pop.Family.Attitude.Current < maxValue;
    }
}

public class SetPopCountDec : IOperation
{
    private float percent;

    public SetPopCountDec(float percent)
    {
        this.percent = percent;
    }

    public void Do(string desc, object target)
    {
        var pop = target as Pop;
        pop.Count -= pop.Count * percent;
    }

    public override string ToString()
    {
        return $"SetPopCountDec -{percent * 100}%";
    }
}

public class ChangeFamilyAttitude : IOperation
{
    private float value;

    public ChangeFamilyAttitude(float value)
    {
        this.value = value;
    }

    public void Do(string desc, object target)
    {
        var pop = target as Pop;

        var attitude = pop.Family.Attitude as Attitude;
        attitude.items.Add(new Attitude.Item(desc, value));
    }

    public override string ToString()
    {
        return $"FamilyAttitude Change {(value < 0 ? "-" : "+")}{value}";
    }
}

public class PopMinCountCondition : ICondition
{
    private int minCount;

    public PopMinCountCondition(int minCount)
    {
        this.minCount = minCount;
    }

    public bool IsSatisfied(object obj)
    {
        return ((int)((IPop)obj).Count) >= minCount;
    }

    public override string ToString()
    {
        return $"PopCount more or equal {minCount}";
    }
}

public class TrueCondition : ICondition
{
    public bool IsSatisfied(object target)
    {
        return true;
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
                Def.Operation.Do(Def.Name, Target);
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

class City : ICity
{
    public static Action<bool, City>? OnOwnerChanged;
    public static Action<IBuffer, ICity>? OnBufferAdded;
    public static Action<IBuffer, ICity>? OnBufferRemoved;

    public string Name { get; }
    public IEffectValue PopTax => popTax;
    public IGroupValue PopCount => popCount;
    public IEnumerable<IPop> Pops => pops;
    public IEnumerable<IBuffer> Buffers => buffers;
    public IEnumerable<IEffect> Effects => buffers.SelectMany(x => x.Effects);

    public bool IsOwned
    {
        get => isOwned;
        set
        {
            isOwned = value;
            OnOwnerChanged?.Invoke(isOwned, this);
        }
    }

    public IEnumerable<ITaskDef> TaskDefs => def.TaskDefs;


    private PopTax popTax;
    private GroupValue popCount;

    private bool isOwned;
    private List<Pop> pops = new List<Pop>();
    private List<Buffer> buffers = new List<Buffer>();

    private ICityDef def;

    public City(ICityDef def, ICityInitData initData, IEnumerable<Pop> pops)
    {
        this.def = def;

        Name = initData.CityName;
        IsOwned = initData.IsControlled;

        popTax = new PopTax(this);
        popCount = new GroupValue();
        popCount.Items = pops.Where(x => x.IsRegisted).Select(x => (x.Name, x.Count));

        this.pops.AddRange(pops);
    }

    internal void OnDaysInc(Date date)
    {
        for (int i = buffers.Count - 1; i >= 0; i--)
        {
            var currBuff = buffers[i];
            if (currBuff.def.InvalidCondition.IsSatisfied(this))
            {

                buffers.Remove(currBuff);

                OnBufferRemoved?.Invoke(currBuff, this);
            }
        }

        foreach (var def in def.BufferDefs)
        {
            if (buffers.Any(x => x.def == def))
            {
                continue;
            }

            if (!def.ValidCondition.IsSatisfied(this))
            {
                continue;
            }

            var newBuff = new Buffer(def);
            buffers.Add(newBuff);

            OnBufferAdded?.Invoke(newBuff, this);
        }
    }
}

class Buffer : IBuffer
{
    public IBufferDef def { get; }

    public string Name => def.BufferName;

    public IEnumerable<IEffect> Effects => def.Effects;

    public Buffer(IBufferDef def)
    {
        this.def = def;
    }
}

class GroupValue : IGroupValue
{
    public float Current => Items.Sum(x => x.count);

    public virtual IEnumerable<(string desc, float count)> Items { get; set; }
}

class Pop : IPop
{
    public string Name => def.PopName;
    public float Count { get; set; }
    public bool IsRegisted => def.IsRegisted;

    public IFamily Family => family;

    public IEnumerable<ITaskDef> TaskDefs => def.TaskDefs;

    private Family? family;

    private IPopDef def;

    public Pop(IPopDef def, IPopInitData popInitData)
    {
        this.def = def;
        this.Count = popInitData.Count;

        if (def.HasFamily)
        {
            family = new Family(popInitData.FamilyName, new[] { ("Test", 10f) });
        }
    }
}

class Family : IFamily
{
    public string Name { get; }

    public IGroupValue Attitude => attitude;

    private Attitude attitude = new Attitude();

    public Family(string name, IEnumerable<(string desc, float count)> attitudeItems)
    {
        this.Name = name;
        this.attitude.items.AddRange(attitudeItems.Select(x => new Attitude.Item(x.desc, x.count)));
    }
}

class Attitude : IGroupValue
{
    internal class Item
    {
        public string Desc { get; }
        public float Value { get; }

        public Item(string desc, float value)
        {
            Desc = desc;
            Value = value;
        }
    }

    public IEnumerable<(string desc, float count)> Items => items.Select(x => (x.Desc, x.Value));

    public float Current => items.Sum(x => x.Value);

    internal readonly List<Item> items = new List<Item>();
}

class PopTax : IEffectValue
{
    public float BaseValue => from.PopCount.Current / 10000f;

    public IEnumerable<IEffect> Effects => from.Effects.OfType<PopTaxChangePercentEffect>();

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

class Date : IDate
{
    public int Year { get; private set; }

    public int Month { get; private set; }

    public int Day { get; private set; }

    public Date()
    {
        Year = 1;
        Month = 1;
        Day = 1;
    }

    public void DaysInc()
    {
        Day++;

        if (Day > 30)
        {
            Month += 1;
            Day = 1;
        }

        if (Month > 12)
        {
            Year += 1;
            Month = 1;
        }
    }
}

public class PopDef : IPopDef
{
    public string PopName { get; init; }

    public bool IsRegisted { get; init; }

    public bool HasFamily { get; init; }

    public IEnumerable<ITaskDef> TaskDefs { get; init; }
}

public class PopInitData : IPopInitData
{
    public int Count { get; init; }

    public string FamilyName { get; init; }

    public string PopName { get; init; }
}

public class PopTaxChangePercentEffect : IEffect
{
    public string Desc { get; init; }

    public float Percent { get; init; }

    public PopTaxChangePercentEffect(string from, float percent)
    {
        Desc = from;
        Percent = percent;
    }
}