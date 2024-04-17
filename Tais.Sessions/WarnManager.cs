using System.Collections;
using Tais.Entities;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Sessions;

internal class WarnManager : IEnumerable<IWarn>
{
    IEnumerable<IWarn> Warns
    {
        get
        {
            foreach (var warn in warns)
            {
                warn.contexts.RemoveAll(x => !warn.Def.Condition.IsSatisfied(x));
            }

            warns.RemoveAll(x => x.contexts.Count == 0);

            foreach (var entity in Entity.GetAll())
            {
                foreach (var def in entity.Def.WarnDefs
                    .Except(warns.Where(warn => warn.contexts.Any(ct => ct.current == entity)).Select(x => x.Def)))
                {
                    var context = new ProcessContext() { current = entity };

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
            warn = new Warn(def, context);
            return;
        }

        warn.contexts.Add(context);
    }
}

internal class Warn : IWarn
{
    public Warn(IWarnDef def, ProcessContext context)
    {
        Def = def;
        contexts.Add(context);
    }

    public IWarnDef Def { get; }

    public IEnumerable<string> Items => contexts.Select(x => x.current.Id);

    internal List<ProcessContext> contexts = new List<ProcessContext>();
}