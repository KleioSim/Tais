using Godot;

public partial class ProductTopInfoItemView : ViewControl, IItemView
{
    public object Id { get; set; }

    public Label Type => GetNode<Label>("HBoxContainer/Type");
    public Label Value => GetNode<Label>("HBoxContainer/Value");
    public Label Surplus => GetNode<Label>("HBoxContainer/Surplus");
}
