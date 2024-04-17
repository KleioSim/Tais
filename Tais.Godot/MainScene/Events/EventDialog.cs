using Godot;

public partial class EventDialog : ViewControl
{
    public object Object;

    public Label Title =>GetNode<Label>("PanelContainer/VBoxContainer/Title");
    public Label Desc => GetNode<Label>("PanelContainer/VBoxContainer/Desc");

    public Button Option => GetNode<Button>("PanelContainer/VBoxContainer/PanelContainer/VBoxContainer/Button");
}