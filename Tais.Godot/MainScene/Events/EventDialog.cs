using Godot;

public partial class EventDialog : ViewControl
{
    public object Object;

    public Label Title =>GetNode<Label>("label");
    public Label Desc => GetNode<Label>("label");

    public Button Option => GetNode<Button>("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/Button");
}