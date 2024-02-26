using Godot;

public partial class EventDialog : ViewControl
{
    public object Object;

    public Button Option => GetNode<Button>("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/Button");
}