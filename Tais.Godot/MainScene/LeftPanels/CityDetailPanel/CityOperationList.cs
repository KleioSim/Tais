using Godot;
using System;

public partial class CityOperationList : ViewControl
{
    public Func<object> GetTarget { get; set; }

    public ItemContainer<CityOperationItem> OperationContainer;

    public override void _Ready()
    {
        OperationContainer = new ItemContainer<CityOperationItem>(() =>
        {
            return GetNode<InstancePlaceholder>("Item");
        });

        OperationContainer.OnAddedItem = (item) =>
        {
            item.GetTarget = GetTarget;
        };
    }
}