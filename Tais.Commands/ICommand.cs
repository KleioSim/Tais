namespace Tais.Commands;

public interface ICommand
{
    string Reason { get; set; }
    object Target { get; set; }
    string Desc { get; }
}

public static class CommandSender
{
    public static Action<ICommand> Send { get; set; }
}