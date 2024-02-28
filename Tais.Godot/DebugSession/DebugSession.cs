using Godot;
using Tais.Interfaces;

public partial class DebugSession : ViewControl
{
    public override void _Process(double delta)
    {
        base._Process(delta);

        GetTree().ChangeSceneToFile("res://MainScene/MainScene.tscn");
    }
}