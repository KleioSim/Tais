namespace Tais.Commands;

public class Cmd_TaskStart : ICommand
{
    public object Def { get; }
    public object Target { get; }

    public Cmd_TaskStart(object def, object target)
    {
        this.Def = def;
        this.Target = target;
    }
}

public class Cmd_NextDay : ICommand
{

}