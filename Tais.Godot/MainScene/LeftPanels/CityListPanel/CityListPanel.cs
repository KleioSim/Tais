using Godot;
using System;

public partial class CityListPanel : LeftContentPanel
{
    internal ItemContainer<CityListItem> ItemContainer;

    public override void _Ready()
    {
        base._Ready();

        ItemContainer = new ItemContainer<CityListItem>(() =>
        {
            return GetNode<InstancePlaceholder>("VBoxContainer/VBoxContainer/CityListItem");
        });
    }
}
