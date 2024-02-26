using Godot;
using System.Collections.Generic;

public abstract partial class PresentBase : Control
{
    public static bool IsMock { get; set; } = true;

    protected static MockManager mockManager = new MockManager();

    protected static HashSet<PresentBase> list = new HashSet<PresentBase>();

    internal bool IsDirty { get; set; }

    public PresentBase()
    {
        this.TreeEntered += () =>
        {
            list.Add(this);
        };

        this.TreeExiting += () =>
        {
            list.Remove(this);
        };
    }
}
