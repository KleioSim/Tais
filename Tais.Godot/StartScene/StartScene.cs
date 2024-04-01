using Godot;
using System;
using Tais.Modders;

public partial class StartScene : Control
{
    public static Action OnNewGame { get; set; }

    public Button Start => GetNode<Button>("VBoxContainer/Start");

    static StartScene()
    {
        OnNewGame += () => GD.Print("StartScene OnNewGame");
    }

    public override void _Ready()
    {
        base._Ready();

        Start.Pressed += () => OnNewGame?.Invoke();
    }
}