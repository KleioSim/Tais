using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

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
        if (def.CommandBuilder == null)
        {
            return;
        }

        var command = def.CommandBuilder.Build(owner.target);

        if (def.CommandBuilder is ICommandWithTarget commandWithTarget)
        {
            commandWithTarget.Target = owner.target;
        }

        CommandSender.Send(command);
    }
}
