using Godot;
using System;

public partial class WarnItem : ViewControl, IItemView
{
    public object Id { get; set; }

    public Control CountPanel => GetNode<Control>("Count");
    public Label Count => CountPanel.GetNode<Label>("Label");

    public TooltipTrigger TooltipTrigger => GetNode<TooltipTrigger>("TooltipTrigger");
}
