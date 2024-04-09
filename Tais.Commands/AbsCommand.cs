namespace Tais.Commands;

public abstract class AbsCommand : ICommandWithTarget
{
    public object Target { get; set; }
    public string Reason { get; set; }
    public abstract string Desc { get; }
}