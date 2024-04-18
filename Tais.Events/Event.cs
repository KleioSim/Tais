﻿using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Events;

internal class Event : IEvent
{
    public string Title => def.Title;
    public string Desc => def.Desc;

    public IOpition Opition => opition;

    internal IEventDef def { get; }
    internal object target { get; }
    private Opition opition;

    public Event(IEventDef def, object target)
    {
        this.def = def;
        this.target = target;
        this.opition = new Opition(this);
    }
}

class Opition : IOpition
{
    private Event owner;
    private IOpitionDef def => owner.def.Opition;
    public string Desc => def.Desc;

    public Opition(Event owner)
    {
        this.owner = owner;
    }

    public void OnSelect()
    {
        if (def.Command == null)
        {
            return;
        }

        if (def.Command is ICommandWithTarget commandWithTarget)
        {
            commandWithTarget.Target = owner.target;
        }

        CommandSender.Send(def.Command);
    }
}

internal class EventProcess
{
    public static ISession Session { get; set; }

    internal static IEnumerable<IEvent> Do<T>(IEnumerable<IEventDef> eventDefs, T target)
        where T : IEntity
    {
        foreach (var eventDef in eventDefs)
        {
            if (!eventDef.VaildDate.Check(Session.Date))
            {
                continue;
            }

            var context = new ProcessContext { current = target, session = Session };
            if (eventDef.TriggerCondition.IsSatisfied(context))
            {
                yield return new Event(eventDef, target);
            }
        }
    }
}

