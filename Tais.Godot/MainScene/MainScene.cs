using Godot;

public partial class MainScene : ViewControl
{
    internal Button CreateTask => GetNode<Button>("CreateTask");
    internal Button CreateEvent { get; }
    internal Button NextTurn => GetNode<Button>("NextTurn");

    internal Button City => GetNode<Button>("TopInfo/City");
    internal Label CityCount => GetNode<Label>("TopInfo/City/HBoxContainer/Count");

    internal Label FinanceCurrent => GetNode<Label>("TopInfo/Finance/HBoxContainer/Current");
    internal Label FinanceSurplus => GetNode<Label>("TopInfo/Finance/HBoxContainer/Surplus");
    internal Label PopCount => GetNode<Label>("TopInfo/Pop/HBoxContainer/Count");

    internal LeftPanel LeftPanel => GetNode<LeftPanel>("LeftPanel");
    internal InstancePlaceholder EventDialogHolder => GetNode<InstancePlaceholder>("EventDialog");

    internal ItemContainer<TaskItem> TaskContainer;

    public override void _Ready()
    {
        TaskContainer = new ItemContainer<TaskItem>(() =>
        {
            return GetNode<InstancePlaceholder>("VBoxContainer/TaskItem");
        });

        City.Pressed += () =>
        {
            var cityListPanel = LeftPanel.ShowCityListPanel();
        };
    }
}
