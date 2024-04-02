using Godot;
using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

public partial class Global : Node
{
    public ISession session;
    public IModder modder;

    public override void _Ready()
    {
        CommandSender.Send += (command) =>
        {
            GD.Print($"CommandSender Send:{command.GetType()}");

            session.OnCommand(command as ICommand);
        };

        PresentBase.SendCommand += (command) =>
        {
            if (command is not Cmd_UIRefresh)
            {
                GD.Print($"UI Send:{command.GetType()}");

                session.OnCommand(command as ICommand);
            }
        };
    }
}
