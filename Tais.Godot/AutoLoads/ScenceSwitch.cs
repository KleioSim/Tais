using Godot;

public partial class ScenceSwitch : Node
{
    public override void _Ready()
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