using Godot;
using System;

public partial class InitialScene : Control
{
    public static Action OnFinished { get; set; }

    static InitialScene()
    {
        OnFinished += () => GD.Print("InitialScene OnFinished");
    }

    public override void _Ready()
    {
        base._Ready();
    }

    public override void _Process(double delta)
    {
        OnFinished.Invoke();
    }
}