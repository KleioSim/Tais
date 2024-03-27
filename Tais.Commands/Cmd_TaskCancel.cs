namespace Tais.Commands;

public class Cmd_TaskCancel : ICommand
{
    public object Target { get; set; }
    public string Reason { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public Cmd_TaskCancel(object task)
    {
        this.Target = task;
    }
}