namespace Tais.Commands;

public class Cmd_TaskCancel : ICommand
{
    public object task { get; }

    public Cmd_TaskCancel(object task)
    {
        this.task = task;
    }
}