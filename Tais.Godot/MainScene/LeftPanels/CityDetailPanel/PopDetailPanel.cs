using Godot;
using System;

public partial class PopDetailPanel : ViewControl
{
    public object Id { get; set; }

    public Label PopName => GetNode<Label>("Panel/PanelContainer/HBoxContainer/PopItem/VBoxContainer/Name/VBoxContainer/Name");
    public Label PopCount => GetNode<Label>("Panel/PanelContainer/HBoxContainer/PopItem/VBoxContainer/Count/VBoxContainer/CurrentValue");
    public Label FamilyName => GetNode<Label>("Panel/PanelContainer/HBoxContainer/PopItem/VBoxContainer/Name/VBoxContainer/Family");

    public Control PopTax => GetNode<Label>("Panel/PanelContainer/HBoxContainer/PanelContainer/HFlowContainer/PopTax/HBoxContainer/Value");

    public Control AttitudePanel => GetNode<Control>("Panel/PanelContainer/HBoxContainer/PanelContainer/HFlowContainer/Attitude");
    public Label Attitude => AttitudePanel.GetNode<Label>("HBoxContainer/Value");

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
