using Godot;
using System;

public partial class MoveControl : Control
{
    [Signal]
    public delegate void CanvasMoveEventHandler(float angle);

    bool isMoveOn;
    float moveAngle;

    public void OnMouseEnterControl()
    {
        isMoveOn = true;
    }

    public void OnMouseExitControl()
    {
        isMoveOn = false;
    }

    public void OnControlGUIInput(InputEvent @event)
    {
        if (!isMoveOn)
        {
            return;
        }

        if (@event is InputEventMouseMotion moveEvent)
        {

            moveAngle = (GlobalPosition + (Size/2)).AngleToPoint(moveEvent.GlobalPosition);

            GD.Print($"{moveAngle})");

        }
    }

    public override void _Process(double delta)
    {
        if(!isMoveOn)
        {
            return;
        }

        EmitSignal(SignalName.CanvasMove, moveAngle);


    }
}
