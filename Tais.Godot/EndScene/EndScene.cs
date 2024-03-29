public partial class EndScene : ViewControl
{
    public void OnReturn()
    {
        GetTree().ChangeSceneToFile("res://StartScene/StartScene.tscn");
    }
}