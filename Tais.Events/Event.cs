using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Events;

internal class Event : IEvent
{
    public string Title => def.Title;
    public string Desc => def.Desc;

    private IEventDef def;
    private object target;

    public Event(IEventDef def, object target)
    {
        this.def = def;
        this.target = target;
    }

    public void OnSelect()
    {
        if (def.Command == null)
        {
            return;
        }

        if (def.Command is ICommandWithTarget commandWithTarget)
        {
            commandWithTarget.Target = target;
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

