using System;
using System.Collections.Generic;
using System.Linq;
using Tais.Commands;
using Tais.Interfaces;


public class SessionProxyMock : ISessionProxy
{
    public IEvent CurrEvent { get; private set; }

    public IEnumerable<ITask> Tasks => tasks;
    public IFinance Finance => finance;

    internal FinanceMock finance = new FinanceMock();
    internal List<TaskMock> tasks = new List<TaskMock>();

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

    public SessionProxyMock()
    {
        EventMock.OnSelected = () =>
        {
            CurrEvent = null;
        };
    }
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