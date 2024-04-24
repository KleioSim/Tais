using Godot;
using System;

public partial class PopItem : ViewControl, IItemView
{
    public object Id { get; set; }

    public Label PopName => GetNode<Label>("HBoxContainer/Main/Name/VBoxContainer/Name");
    public Label PopCount => GetNode<Label>("HBoxContainer/Main/Count/VBoxContainer/CurrentValue");
    public Label FamilyName => GetNode<Label>("HBoxContainer/Main/Name/VBoxContainer/Family");

    public Control Attitude => GetNode<Control>("HBoxContainer/Sub/Attitude");
    public Label AttitudeLabel => Attitude.GetNode<Label>("HBoxContainer/Value");

    public Control Living => GetNode<Control>("HBoxContainer/Sub/Living");
    public Label LivingLabel => Living.GetNode<Label>("HBoxContainer/Value");

    public Button Button => GetNode<Button>("Button");
}
