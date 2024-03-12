using Godot;
using System;

public partial class CityDetailPanel : LeftContentPanel1
{
    public object Id { get; internal set; }

    public override Button CloseButton => GetNode<Button>("HBoxContainer/VBoxContainer2/CityName/Close");

    public Label CityName => GetNode<Label>("HBoxContainer/VBoxContainer2/CityName");
    public Label PopCount => GetNode<Label>("HBoxContainer/VBoxContainer2/VBoxContainer/Infos/MarginContainer/HFlowContainer/PopCount/HBoxContainer/Value");
    public Label PopTax => GetNode<Label>("HBoxContainer/VBoxContainer2/VBoxContainer/Infos/MarginContainer/HFlowContainer/PopTax/HBoxContainer/Value");

    public Button OperateButton => GetNode<Button>("HBoxContainer/VBoxContainer2/CityName/Operate");

    public Control OperatePanel => GetNode<Control>("HBoxContainer/VBoxContainer");

    public ItemContainer<PopItem> PopContainer;

    public override void _Ready()
    {
        base._Ready();

        PopContainer = new ItemContainer<PopItem>(() =>
        {
            return GetNode<InstancePlaceholder>("HBoxContainer/VBoxContainer2/VBoxContainer/Pops/FlowContainer/PopItem");
        });

        OperatePanel.Visible = OperateButton.ButtonPressed;
        OperateButton.Toggled += (flag) =>
        {
            OperatePanel.Visible = flag;
        };
    }
}
