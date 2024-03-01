using System;
using System.Collections.Generic;
using Tais.Commands;
using Tais.Interfaces;


public class SessionProxyMock : ISession
{
    public IEnumerable<ITask> Tasks => tasks;

    public IEvent CurrEvent { get; private set; }

    private List<Task> tasks = new List<Task>();

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
                    tasks.Add(new Task());
                }
                break;
            case Cmd_NextTurn:
                {
                    CurrEvent = new Event();
                }
                break;
        }
    }

    public SessionProxyMock()
    {
        Event.OnSelected = () =>
        {
            CurrEvent = null;
        };
    }
}

public class Event : IEvent
{
    public static Action OnSelected;

    public void OnSelect()
    {
        OnSelected.Invoke();
    }
}

public class Task : ITask
{

}