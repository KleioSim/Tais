using Godot;
using Godot.Collections;
using System;
using System.Linq;

[Tool]
public partial class LocalLabel : Label
{
    public static Func<string, string> GetLocalString
    {
        get => _GetLocalString;
        set
        {
            _GetLocalString = value;
            GD.Print($"set GetLocalString {_GetLocalString}");
        }
    }

    private static Func<string, string> _GetLocalString;

    private string _Id;


    [Export]
    public string TextId
    {
        get => _Id;
        set
        {
            _Id = value;
            GD.Print($"TextId Set {value} {_GetLocalString}");
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