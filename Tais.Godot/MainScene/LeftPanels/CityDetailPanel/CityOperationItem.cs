using Godot;
using System;

public partial class CityOperationItem : ViewControl, IItemView
{
    public object Id { get; set; }

    public Func<object> GetTarget { get; set; }

    public Button Button => GetNode<Button>("Button");
}
