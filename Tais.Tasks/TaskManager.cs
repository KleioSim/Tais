using System;
using System.Collections;
using System.Collections.Generic;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Tasks;

class TaskManager : IEnumerable<Task>
{
    private List<Task> tasks = new List<Task>();
    private ISession session;

    public TaskManager(ISession session)
    {
        this.session = session;
    }

    public IEnumerator<Task> GetEnumerator()
    {
        return ((IEnumerable<Task>)tasks).GetEnumerator();
    }

    internal void Add(ITaskDef taskDef, object target)
    {
        var task = new Task(taskDef, target);
        tasks.Add(task);
    }

    internal void OnDaysInc(IDate date)
    {
        foreach (var task in tasks)
        {
            task.OnDaysInc(date);
        }

        tasks.RemoveAll(x => x.Progress >= 100);
    }

    internal void Remove(Task? task)
    {
        tasks.Remove(task);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)tasks).GetEnumerator();
    }
}