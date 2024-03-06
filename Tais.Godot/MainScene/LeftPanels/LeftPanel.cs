using Godot;
using System;
using System.Linq;

public partial class LeftPanel : ViewControl
{
    internal Node Panel0 => GetNode("0");
    internal Node Panel1 => GetNode("1");

    internal T CreatePanel<T>()
        where T : LeftContentPanel
    {
        T panel = CreatePanel0<T>();
        if (panel != null)
        {
            return panel;
        }

        panel = CreatePanel1<T>();
        if (panel != null)
        {
            return panel;
        }

        throw new Exception();
    }

    internal T CreatePanel0<T>()
        where T : LeftContentPanel
    {
        var panel = Panel0.GetChildren().OfType<T>().SingleOrDefault();
        if (panel != null)
        {
            return panel;
        }

        var placeHolder = Panel0.GetNodeOrNull<InstancePlaceholder>($"{typeof(T).Name}");
        return placeHolder == null ? null : placeHolder.CreateInstance() as T;
    }

    internal T CreatePanel1<T>()
        where T : LeftContentPanel
    {
        var panel = Panel1.GetChildren().OfType<T>().SingleOrDefault();
        if (panel != null)
        {
            return panel;
        }

        var placeHolder = Panel1.GetNodeOrNull<InstancePlaceholder>($"{typeof(T).Name}");
        return placeHolder == null ? null : placeHolder.CreateInstance() as T;
    }
}
