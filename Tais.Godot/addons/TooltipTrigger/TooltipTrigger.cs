using Godot;
using System;

public partial class TooltipTrigger : Control
{
    public Func<string> funcGetToolTipString;

    public override string _GetTooltip(Vector2 atPosition)
    {
        return funcGetToolTipString != null ? funcGetToolTipString() : base._GetTooltip(atPosition);
    }
}
