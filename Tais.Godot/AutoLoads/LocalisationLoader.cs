using Godot;
using System;

[Tool]
public partial class LocalisationLoader : Node
{
    public override void _Process(double delta)
    {
        LocalLabel.GetLocalString ??= (id) =>
        {
            if (id == "123") return "abc";
            if (id == "COUNTRY") return "县区";
            if (id == "POP") return "户口";
            if (id == "FINANCE") return "财政";
            if (id == "YEAR") return "年";
            if (id == "MONTH") return "月";
            if (id == "DAY") return "日";
            return id;
        };
    }

}
