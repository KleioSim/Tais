using Tais.Commands;
using Tais.Interfaces;

public partial class TaskItemPresent : PresentControl<TaskItem, ISession>
{
    protected override void Initialize(TaskItem view, ISession model)
    {
        view.Cancel.Pressed += () =>
        {
            SendCommand(new Cmd_TaskCancel(view.Id));
        };
    }

    protected override void Update(TaskItem view, ISession model)
    {

    }
}