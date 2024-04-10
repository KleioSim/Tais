using Godot;
using System;

public partial class CityDetailPanel : LeftContentPanel1
{
    public object Id { get; set; }

    public override Button CloseButton => GetNode<Button>("HBoxContainer/VBoxContainer2/CityName/Close");

    public Label DebugID => GetNode<Label>("HBoxContainer/VBoxContainer2/CityName/DebugID");
    public Label CityName => GetNode<Label>("HBoxContainer/VBoxContainer2/CityName");
    public Label PopCount => GetNode<Label>("HBoxContainer/VBoxContainer2/VBoxContainer/Infos/MarginContainer/HFlowContainer/PopCount/HBoxContainer/Value");

    public Label PopTax => GetNode<Label>("HBoxContainer/VBoxContainer2/VBoxContainer/Infos/MarginContainer/HFlowContainer/PopTax/HBoxContainer/Value");
    public TooltipTrigger PopTaxToolTipTrigger => PopTax.GetNode<TooltipTrigger>("TooltipTrigger");

    public Button OperateButton => GetNode<Button>("HBoxContainer/VBoxContainer2/CityName/Operate");
    public CityOperationList OperationList => GetNode<CityOperationList>("HBoxContainer/CityOperationList");

    public PopDetailPanel PopDetailPanel => GetNode<PopDetailPanel>("HBoxContainer/VBoxContainer2/VBoxContainer/Pops/PopDetailPanel");

    public ItemContainer<PopItem> PopContainer;
    public ItemContainer<BufferItem> BufferContainer;

    public override void _Ready()
    {
        base._Ready();

        PopContainer = new ItemContainer<PopItem>(() =>
        {
            return GetNode<InstancePlaceholder>("HBoxContainer/VBoxContainer2/VBoxContainer/Pops/FlowContainer/PopItem");
        });

        BufferContainer = new ItemContainer<BufferItem>(() =>
        {
            return GetNode<InstancePlaceholder>("HBoxContainer/VBoxContainer2/VBoxContainer/Buffers/HFlowContainer/BufferItem");
        });

        PopDetailPanel.Visible = false;

        OperationList.GetTarget = () => Id;

        OperationList.Visible = OperateButton.ButtonPressed;
        OperateButton.Toggled += (flag) =>
        {
            OperationList.Visible = flag;
        };
    }
}
