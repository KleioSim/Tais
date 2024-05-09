using Godot;
using System;
using System.IO;
using System.Reflection;
using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;


public partial class Global : Node
{
    public static string ModPath
    {
        get
        {
            var path = Path.Combine(Path.GetDirectoryName(OS.GetExecutablePath()), "mods");
            if (OS.HasFeature("editor"))
            {
                path = Path.Combine(ProjectSettings.GlobalizePath("res://"), ".mods");
            }

            return path;
        }
    }

    public ISession session;
    public IModder modder;

    public override void _Ready()
    {
        var path = System.AppDomain.CurrentDomain.BaseDirectory;

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

    public override void _EnterTree()
    {
        GD.Print("Global _EnterTree");
    }

    public override void _ExitTree()
    {
        GD.Print("Global _ExitTree");
    }
}
