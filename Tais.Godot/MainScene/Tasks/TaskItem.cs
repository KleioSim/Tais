using Godot;

public partial class TaskItem : ViewControl, IItemView
{
    public object Id { get; set; }

    public Button Cancel => GetNode<Button>("Cancel");
}