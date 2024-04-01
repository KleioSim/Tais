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

        InitialSwitch();


    }

    public override void _Process(double delta)
    {
        OnFinished.Invoke();
    }

    private static void InitialSwitch()
    {
        MainScene.OnGMFailed += () =>
        {
            GetTree().ChangeSceneToFile("res://StartScene/StartScene.tscn");
        };
        MainScene.OnGMSuccessed += () =>
        {
            GetTree().ChangeSceneToFile("res://StartScene/StartScene.tscn");
        };

        StartScene.OnNewGame += () =>
        {
            GetTree().ChangeSceneToFile("res://NewGameScene/NewGameScene.tscn");
        };

        InitialScene.OnFinished += () =>
        {
            GetTree().ChangeSceneToFile("res://StartScene/StartScene.tscn");
        };

        NewGameScene.OnEnterGame += () =>
        {
            GetTree().ChangeSceneToFile("res://MainScene/MainScene.tscn");
        };

        NewGameScene.OnBack = () =>
        {
            GetTree().ChangeSceneToFile("res://StartScene/StartScene.tscn");
        };
    }
}