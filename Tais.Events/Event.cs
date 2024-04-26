using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Events;

internal class Event : IEvent
{
    public string Title => def.Title.Build(context);
    public string Desc => def.Desc;

    public IOpition Opition => opition;

    internal IEventDef def { get; }
    internal ProcessContext context { get; }
    private Opition opition;

    public Event(IEventDef def, ProcessContext context)
    {
        this.def = def;
        this.context = context;
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
        if (def.CommandBuilder == null)
        {
            return;
        }

        var command = def.CommandBuilder.Build(owner.context);

        if (def.CommandBuilder is ICommandWithTarget commandWithTarget)
        {
            commandWithTarget.Target = owner.context.current;
        }

        CommandSender.Send(command);
    }
}
