namespace Tais.Commands;

public class Cmd_TaskCancel : AbsCommand
{
    public override string Desc { get; }

    public Cmd_TaskCancel(object task)
    {
        this.Target = task;
    }
}