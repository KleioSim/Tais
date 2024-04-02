using Godot;
using System;

public partial class NewGameScene : Control
{
    public static Action OnEnterGame { get; set; }
    public static Action OnBack { get; set; }

    public Button EnterGame => GetNode<Button>("VBoxContainer/Enter");
    public Button Back => GetNode<Button>("VBoxContainer/Back");

    static NewGameScene()
    {
        OnEnterGame += () => GD.Print("NewGameScene OnEnterGame");
        OnBack += () => GD.Print("NewGameScene OnBack");
    }

    public override void _Ready()
    {
        base._Ready();

        EnterGame.Pressed += () => OnEnterGame.Invoke();
        Back.Pressed += () => OnBack.Invoke();
    }

}
