using Godot;

public partial class BufferItem : ViewControl, IItemView
{
    public object Id { get; set; }

    public TooltipTrigger ToolTipTrigger => GetNode<TooltipTrigger>("TooltipTrigger");

    //public Label BufferName => GetNode<Label>("");
}