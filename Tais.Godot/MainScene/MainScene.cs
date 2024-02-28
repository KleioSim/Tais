using Godot;

public partial class MainScene : ViewControl
{
    internal Button CreateTask => GetNode<Button>("CreateTask");
    internal Button CreateEvent { get; }
    internal Button NextTurn => GetNode<Button>("NextTurn");

    internal Label FinanceCurrent => GetNode<Label>("Finance/HBoxContainer/Current");
    internal Label FinanceSurplus => GetNode<Label>("Finance/HBoxContainer/Surplus");

    internal InstancePlaceholder EventDialogHolder => GetNode<InstancePlaceholder>("EventDialog");

    internal ItemContainer<TaskItem> TaskContainer;

    public override void _Ready()
    {
        TaskContainer = new ItemContainer<TaskItem>(() =>
        {
            return GetNode<InstancePlaceholder>("VBoxContainer/TaskItem");
        });
    }
}
