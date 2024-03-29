using Godot;
using System;

public partial class TooltipTriggerEx : TooltipTrigger
{
    public override Control _MakeCustomTooltip(string forText)
    {
        Control tooltip = ResourceLoader.Load<PackedScene>("res://Utilities/TooltipPanel/TooltipPanel.tscn").Instantiate() as Control;
        tooltip.GetNode<RichTextLabel>("RichTextLabel").Text = forText;
        return tooltip;
    }
}
