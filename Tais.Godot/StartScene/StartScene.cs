public partial class StartScene : ViewControl
{
    public void OnNew()
    {
        GetTree().ChangeSceneToFile("res://InitializeScene/InitializeScene.tscn");
    }
}