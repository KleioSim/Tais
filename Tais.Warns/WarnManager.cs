using System.Collections;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Warns;

internal class WarnManager : IEnumerable<IWarn>
{
    public readonly ISession session;

    IEnumerable<IWarn> Warns
    {
        get
        {
            foreach (var warn in warns)
            {
                warn.contexts.RemoveAll(x => !warn.Def.Condition.IsSatisfied(x));
            }

            warns.RemoveAll(x => x.contexts.Count == 0);

            foreach (var entity in session.Entities)
            {
                foreach (var def in entity.Def.WarnDefs
                    .Except(warns.Where(warn => warn.contexts.Any(ct => ct.current == entity)).Select(x => x.Def)))
                {
                    var context = new ProcessContext() { current = entity, session = session };

                    if (def.Condition.IsSatisfied(context))
                    {
                        AddWarn(def, context);
                    }
                }
            }

            return warns;
        }
    }

    private List<Warn> warns = new List<Warn>();

    public WarnManager(ISession session)
    {
        this.session = session;
    }

    public IEnumerator<IWarn> GetEnumerator()
    {
        return Warns.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)Warns).GetEnumerator();
    }

    private void AddWarn(IWarnDef def, ProcessContext context)
    {
        var warn = warns.SingleOrDefault(x => x.Def == def);
        if (warn == null)
        {
            warns.Add(new Warn(def, context));
            return;
        }

        warn.contexts.Add(context);
    }
}
