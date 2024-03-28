namespace Tais.Commands;

public abstract class AbsCommand : ICommand
{
    public object Target { get; set; }
    public string Reason { get; set; }
    public abstract string Desc { get; }
}