using Godot;
using System;

public partial class PopItem : ViewControl, IItemView
{
    public object Id { get; set; }

    public Label PopName => GetNode<Label>("VBoxContainer/Name/VBoxContainer/Name");
    public Label PopCount => GetNode<Label>("VBoxContainer/Count/VBoxContainer/CurrentValue");
    public Label FamilyName => GetNode<Label>("VBoxContainer/Name/VBoxContainer/Family");
}
