using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Warns;

internal class Warn : IWarn
{
    public string Name => Def.Name;

    public IWarnDef Def { get; }

    public IEnumerable<string> Items => contexts.Select(x => x.current.Id);

    internal List<ProcessContext> contexts = new List<ProcessContext>();

    public Warn(IWarnDef def, ProcessContext context)
    {
        Def = def;
        contexts.Add(context);
    }
}