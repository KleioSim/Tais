using Godot;
using System;

public partial class PopDetailPanel : ViewControl
{
    public object Id { get;set; }

    public Label PopName => GetNode<Label>("Panel/PanelContainer/HBoxContainer/PopItem/VBoxContainer/Name/VBoxContainer/Name");
    public Label PopCount => GetNode<Label>("Panel/PanelContainer/HBoxContainer/PopItem/VBoxContainer/Count/VBoxContainer/CurrentValue");

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
