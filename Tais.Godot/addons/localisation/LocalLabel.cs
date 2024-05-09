using Godot;
using Godot.Collections;
using System;
using System.Linq;

[Tool]
public partial class LocalLabel : Label
{
    public static Func<string, string> GetLocalString { get; set; }


    private string _Id;

    [Export]
    public string TextId
    {
        get => _Id;
        set
        {
            _Id = value;
            this.Text = GetLocalString != null ? GetLocalString(_Id) : _Id;
        }
    }


    public override void _ValidateProperty(Dictionary property)
    {
        if (property["name"].AsStringName() == "text")
        {
            property["usage"] = (int)PropertyUsageFlags.None;
        }
    }
}