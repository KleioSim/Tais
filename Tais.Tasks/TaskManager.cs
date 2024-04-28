using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

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

    internal void Add(ITaskDef taskDef, IEntity target)
    {
        var task = new Task(taskDef, target);
        tasks.Add(task);
    }

    internal void OnDaysInc(IDate date)
    {
        var finished = new List<Task>();

        foreach (var task in tasks)
        {
            task.Progress += task.Def.Speed;

            if (task.Progress >= 100)
            {
                finished.Add(task);
                foreach (var cmdBuilder in task.Def.CommandBuilders)
                {
                    var cmd = cmdBuilder.Build(new ProcessContext(session, task.Target));

                    cmd.Reason = task.Def.Name;

                    if (cmd is ICommandWithTarget cmdWithTarget)
                    {
                        cmdWithTarget.Target = task.Target;
                    }

                    CommandSender.Send(cmd);
                }
            }
        }

        tasks.RemoveAll(x => finished.Contains(x));
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