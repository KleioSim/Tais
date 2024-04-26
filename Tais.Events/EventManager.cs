using Tais.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Events;

internal class EventManager
{
    private readonly ISession session;

    public EventManager(ISession session)
    {
        this.session = session;
    }

    public IEnumerable<IEvent> OnDaysInc()
    {
        foreach (var entity in session.Entities)
        {
            foreach (var def in entity.Def.EventDefs)
            {
                if (!def.VaildDate.Check(session.Date))
                {
                    continue;
                }

                var context = new ProcessContext() { current = entity, session = session };

                if (def.TriggerCondition.IsSatisfied(context))
                {
                    yield return new Event(def, context);
                }
            }
        }
    }
}
