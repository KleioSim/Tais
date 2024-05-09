#if TOOLS
using Godot;
using System;

[Tool]
public partial class Localisation : EditorPlugin
{
    public override void _EnterTree()
    {
        var script = GD.Load<Script>("res://addons/localisation/LocalLabel.cs");
        var texture = GD.Load<Texture2D>("res://addons/localisation/Icon.png");
        AddCustomType("LocalLabel", "Label", script, texture);
    }

    public override void _ExitTree()
    {
        RemoveCustomType("LocalLabel");
    }
}
#endif
