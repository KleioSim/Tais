using Godot;
using System;

public partial class CityListItem : ViewControl, IItemView
{
    public object Id { get; set; }

    public Button Button => GetNode<Button>("Button");

    public Label CityName => GetNode<Label>("HBoxContainer/CityName");
    public Label PopCount => GetNode<Label>("HBoxContainer/PopCount");
    public Label IsControl => GetNode<Label>("HBoxContainer/IsControl");
}
