using Godot;

public partial class LeftContentPanel0 : ViewControl
{
    public virtual Button CloseButton => GetNode<Button>("Close");

    public override void _Ready()
    {
        CloseButton.Pressed += () =>
        {
            this.QueueFree();
        };
    }
}
