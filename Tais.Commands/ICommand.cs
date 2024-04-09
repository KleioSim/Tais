namespace Tais.Commands;

public interface ICommand
{
    string Reason { get; set; }
    string Desc { get; }
}

public interface ICommandWithTarget : ICommand
{
    object Target { get; set; }
}

public static class CommandSender
{
    public static Action<ICommand> Send { get; set; }
}