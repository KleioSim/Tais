using Godot;
using System;
using Tais.Modders;

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

        PresentBase.IsMock = false;

        var global = GetNode<Global>("/root/Global");
        global.modder = ModderBuilder.Build();
    }

    public override void _Process(double delta)
    {
        OnFinished.Invoke();
    }
}