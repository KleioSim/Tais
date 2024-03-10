using Godot;
using System;
using System.Linq;

public partial class LeftPanel : ViewControl
{
    internal Node Panel0 => GetNode("0");
    internal Node Panel1 => GetNode("1");

    public void OnShowCityDetail(CityListItem cityListItem)
    {
        GD.Print("OnShowCityDetail");
    }

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

    internal CityListPanel ShowCityListPanel()
    {
        var panel = Panel0.GetChildren().OfType<CityListPanel>().SingleOrDefault();
        if (panel != null)
        {
            return panel;
        }

        var placeHolder = Panel0.GetNodeOrNull<InstancePlaceholder>("CityListPanel");
        panel = placeHolder.CreateInstance() as CityListPanel;

        panel.ShowCityDetail += (cityListItem) =>
        {
            ShowCityDetailPanel(cityListItem.Id);
        };

        panel.CloseButton.Pressed += () =>
        {
            var subPanel = Panel1.GetChildren().OfType<LeftContentPanel>().SingleOrDefault();
            if (subPanel != null)
            {
                subPanel.QueueFree();
            }
        };

        return panel;
    }

    private CityDetialPanel ShowCityDetailPanel(object id)
    {
        var panel = Panel1.GetChildren().OfType<CityDetialPanel>().SingleOrDefault();
        if (panel != null)
        {
            return panel;
        }

        var placeHolder = Panel1.GetNodeOrNull<InstancePlaceholder>("CityDetailPanel");
        panel = placeHolder.CreateInstance() as CityDetialPanel;
        panel.Id = id;

        return panel;
    }
}
