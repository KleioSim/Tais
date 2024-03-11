using Godot;
using System;

public partial class CityDetailPanel : LeftContentPanel
{
    public object Id { get; internal set; }

    public override Button CloseButton => GetNode<Button>("HBoxContainer/VBoxContainer2/CityName/Close");
}
