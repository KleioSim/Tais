using Godot;
using Tais.Interfaces;

public partial class EventDialogPresent : PresentControl<EventDialog, ISession>
{
    protected override void Initialize(EventDialog view, ISession model)
    {
        var eventObj = view.Object as IEvent;
        view.Option.Pressed += () =>
        {
            eventObj.Opition.OnSelect();
            view.QueueFree();

            SendCommand(new Cmd_UIRefresh());
        };
    }

    protected override void Update(EventDialog view, ISession model)
    {
        var eventObj = view.Object as IEvent;

        view.Title.Text = eventObj.Title;
        view.Desc.Text = eventObj.Desc;
        view.OptionLabel.Text = eventObj.Opition.Desc;
    }
}