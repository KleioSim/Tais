using Godot;
using System;

public partial class CityDetailPanel : LeftContentPanel1
{
    public object Id { get; internal set; }

    public override Button CloseButton => GetNode<Button>("HBoxContainer/VBoxContainer2/CityName/Close");

    public Label CityName => GetNode<Label>("HBoxContainer/VBoxContainer2/CityName");

    public Button OperateButton => GetNode<Button>("HBoxContainer/VBoxContainer2/CityName/Operate");

    public Control OperatePanel => GetNode<Control>("HBoxContainer/VBoxContainer");

    public override void _Ready()
    {
        base._Ready();

        OperatePanel.Visible = OperateButton.ButtonPressed;
        OperateButton.Toggled += (flag) =>
        {
            OperatePanel.Visible = flag;
        };
    }
}
