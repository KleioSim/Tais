using Godot;
using Tais.Commands;
using Tais.Interfaces;

public partial class Initialize : Node
{
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