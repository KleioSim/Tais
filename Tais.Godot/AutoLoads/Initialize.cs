using Godot;
using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

public partial class Initialize : Node
{
    public ISession session;
    public IModder modder;

    public override void _Ready()
    {
        PresentBase.SendCommand += (command) =>
        {
            if (command is not Cmd_UIRefresh)
            {
                GD.Print($"OnUICommand:{command.GetType()}");

                CommandSender.Send?.Invoke(command as ICommand);
            }
        };
    }
}