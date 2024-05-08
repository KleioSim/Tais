using Godot;
using Godot.Collections;
using System;
using System.Linq;

[Tool]
public partial class LabelExt : Label
{
    private string _Id; // Exported field specifies a default value


    // This property uses `_greeting` as its backing field, so the default value
    // will be the default value of the `_greeting` field.
    [Export]
    public string Id
    {
        get => _Id;
        set
        {
            this.Text = _Id = value;
        }
    }

    //public override void _EnterTree()
    //{
    //    base._EnterTree();

    //    GD.Print("_EnterTree");

    //    if (base.GetPropertyList() != null)
    //    {
    //        foreach (var dict in base.GetPropertyList())
    //        {
    //            if (dict != null && dict.ContainsKey("name") && dict["name"].AsString() == "text")
    //            {
    //                GD.Print("_GetPropertyList text");
    //                if (dict.ContainsKey("usage"))
    //                {
    //                    GD.Print("_GetPropertyList usage");
    //                    GD.Print(dict["usage"]);
    //                    dict["usage"] = (int)PropertyUsageFlags.None;
    //                    GD.Print(dict["usage"]);
    //                }

    //                //GD.Print(dict["usage"].AsInt32());
    //                //dict["usage"] = (int)PropertyUsageFlags.None;
    //            }
    //        }
    //    }
    //}

    //public override void _ExitTree()
    //{
    //    base._ExitTree();

    //    GD.Print("_ExitTree");

    //    if (base.GetPropertyList() != null)
    //    {
    //        foreach (var dict in base.GetPropertyList())
    //        {
    //            if (dict != null && dict.ContainsKey("name") && dict["name"].AsString() == "text")
    //            {
    //                GD.Print("_GetPropertyList text");
    //                if (dict.ContainsKey("usage"))
    //                {
    //                    GD.Print("_GetPropertyList usage");
    //                    GD.Print(dict["usage"]);
    //                    dict["usage"] = (int)PropertyUsageFlags.None;
    //                    GD.Print(dict["usage"]);
    //                }

    //                //GD.Print(dict["usage"].AsInt32());
    //                //dict["usage"] = (int)PropertyUsageFlags.None;
    //            }
    //        }

    //        foreach (var dict in base.GetPropertyList())
    //        {
    //            if (dict != null && dict.ContainsKey("name") && dict["name"].AsString() == "text")
    //            {
    //                ;
    //                if (dict.ContainsKey("usage"))
    //                {
    //                    GD.Print(dict["usage"]);
    //                }

    //                //GD.Print(dict["usage"].AsInt32());
    //                //dict["usage"] = (int)PropertyUsageFlags.None;
    //            }
    //        }
    //    }
    //}

    public override void _ValidateProperty(Dictionary property)
    {
        if (property["name"].AsStringName() == "text")
        {
            GD.Print("text");

            property["usage"] = (int)PropertyUsageFlags.None;
        }
    }

    //public override void _Ready()
    //{
    //    base._Ready();

    //    foreach (var dict in base.GetPropertyList())
    //    {
    //        if (dict["name"].AsString() == "text")
    //        {
    //            int usage = dict["usage"].AsInt32();
    //        }
    //    }
    //}
}
