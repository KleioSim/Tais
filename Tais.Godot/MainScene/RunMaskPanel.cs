using Godot;
using System.Collections.Generic;


public partial class RunMaskPanel : Panel
{
    public static IEnumerable<RunMaskPanel> Panels => list;
    private static List<RunMaskPanel> list = new List<RunMaskPanel>();

    public override void _Ready()
    {
        base._Ready();

        list.Add(this);
    }

    public override void _ExitTree()
    {
        list.Remove(this);

        base._ExitTree();
    }
}