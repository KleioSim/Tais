using Godot;
using Tais.Modders;

public partial class StartScene : Control
{
    public override void _Ready()
    {
        base._Ready();

        var global = GetNode<Initialize>("/root/Global");
        global.modder = ModderBuilder.Build();
    }

    public void OnNew()
    {
        GetTree().ChangeSceneToFile("res://InitializeScene/InitializeScene.tscn");
    }
}