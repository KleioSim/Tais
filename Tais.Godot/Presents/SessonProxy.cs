using System;
using System.Collections.Generic;
using System.Linq;
using Tais.Commands;
using Tais.Interfaces;


public class SessionMock : ISession
{
    public IEvent CurrEvent { get; private set; }

    public IEnumerable<ITask> Tasks => tasks;
    public IFinance Finance => finance;
    public IEnumerable<ICity> Cities => cities;
    public ICentralGov CentralGov => throw new NotImplementedException();
    public IEnumerable<ICityTaskDef> CityTaskDefs => throw new NotImplementedException();

    public IDate Date => throw new NotImplementedException();

    internal FinanceMock finance = new FinanceMock();
    internal List<TaskMock> tasks = new List<TaskMock>();
    internal List<CityMock> cities = new List<CityMock>();

    public void OnCommand(object command)
    {
        OnCommand(command as ICommand);
    }

    public void OnCommand(ICommand command)
    {
        switch (command)
        {
            case Cmd_CreateTask:
                {
                    tasks.Add(new TaskMock());
                }
                break;
            case Cmd_NextTurn:
                {
                    finance.NextTurn();
                    CurrEvent = new EventMock();
                }
                break;
        }
    }

    internal CityMock GenerateCity(string name, int popCount, bool isOwned)
    {
        var city = new CityMock();
        city.Name = name;
        city.IsOwned = isOwned;
        city.popCount.Current = popCount;

        cities.Add(city);

        return city;
    }

    public SessionMock()
    {
        EventMock.OnSelected = () =>
        {
            CurrEvent = null;
        };
    }
}

public class CityMock : ICity
{
    public IGroupValue PopCount => popCount;
    public IEffectValue PopTax => popTax;

    public bool IsOwned { get; set; }

    public string Name { get; set; }

    public IEnumerable<IPop> Pops => throw new NotImplementedException();

    public EffectValueMock popTax = new EffectValueMock();
    public GroupValueMock popCount = new GroupValueMock();
}

public class GroupValueMock : IGroupValue
{
    public float Current { get; set; }

    public IEnumerable<(string desc, float count)> Items => items;

    public List<(string desc, float count)> items = new List<(string desc, float count)>();
}


public class EventMock : IEvent
{
    public static Action OnSelected;

    public void OnSelect()
    {
        OnSelected.Invoke();
    }
}

public class TaskMock : ITask
{
    public ICityTaskDef Def => throw new NotImplementedException();

    public object Target => throw new NotImplementedException();

    public float Progress => throw new NotImplementedException();
}

public class FinanceMock : IFinance
{
    public float Current { get; set; }

    public float Surplus { get; set; }

    public IEnumerable<IEffectValue> Incomes => incomes;

    public IEnumerable<IEffectValue> Spends => spends;

    public List<EffectValueMock> incomes = new List<EffectValueMock>();
    public List<EffectValueMock> spends = new List<EffectValueMock>();

    internal void NextTurn()
    {
        Current += Surplus;
    }
}

public class EffectValueMock : IEffectValue
{
    public float BaseValue { get; set; }

    public IEnumerable<IEffect> Effects => effects;

    public List<EffectMock> effects = new List<EffectMock>();
}

public class EffectMock : IEffect
{
    public string Desc { get; set; }

    public float Percent { get; set; }
}