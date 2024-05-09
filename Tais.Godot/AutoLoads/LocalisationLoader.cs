using Godot;
using System;

[Tool]
public partial class LocalisationLoader : Node
{
    public override void _Ready()
    {
        GD.Print("_Ready");
    }

    public override void _Process(double delta)
    {
        if (LocalLabel.GetLocalString == null)
        {
            GD.Print("_Process LocalLabel.GetLocalString == null");

            LocalLabel.GetLocalString = (id) =>
            {
                if (id == "123") return "abc";
                return id;
            };
        }
    }

    public override void _Notification(int what)
    {
        if (what == NotificationProcess
            || what == NotificationInternalPhysicsProcess
            || what == NotificationPhysicsProcess
            || what == NotificationInternalPhysicsProcess)
        {
            return;
        }

        GD.Print($"_Notification {what}");
    }

}
