using Godot;
using System;

public partial class PopDetailPanel : ViewControl
{
    public object Id { get;set; }

    public Button CloseButton => GetNode<Button>("Panel/PanelContainer/Close");

    public override void _Ready()
    {
        base._Ready();

        CloseButton.Pressed += () =>
        {
            this.Visible = false;
        };
    }
}
