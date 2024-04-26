using Godot;
using System;

public partial class PopItem : ViewControl, IItemView
{
    public object Id { get; set; }

    public Button Button => GetNode<Button>("HBoxContainer/Button");

    public Label PopName => GetNode<Label>("HBoxContainer/Button/VBoxContainer/Name");
    public Label FamilyName => GetNode<Label>("HBoxContainer/Button/VBoxContainer/Family");
    public Label PopCount => GetNode<Label>("HBoxContainer/VBoxContainer/GridContainer/PopCount/HBoxContainer/Value");

    public Control Attitude => GetNode<Control>("HBoxContainer/VBoxContainer/GridContainer/Attitude");
    public Label AttitudeLabel => Attitude.GetNode<Label>("HBoxContainer/Value");
    public TooltipTrigger AttitudeDetail => AttitudeLabel.GetNode<TooltipTrigger>("TooltipTriggerEx");

    public Control Living => GetNode<Control>("HBoxContainer/VBoxContainer/GridContainer/Living");
    public Label LivingLabel => Living.GetNode<Label>("HBoxContainer/Value");
    public TooltipTrigger LivingDetail => LivingLabel.GetNode<TooltipTrigger>("TooltipTriggerEx");
}
