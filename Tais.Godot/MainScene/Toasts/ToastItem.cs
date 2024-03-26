using Godot;
using System;

public partial class ToastItem : ViewControl, IItemView
{
    public object Id { get; set; }

    public Label Desc => GetNode<Label>("Desc");

    public Button Button => GetNode<Button>("Button");

    public override void _Ready()
    {
        base._Ready();

        Button.Pressed += () =>
        {
            QueueFree();
        };
    }
}
