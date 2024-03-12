using Godot;
using System;
using System.Linq;
using System.Threading.Tasks;

public partial class LeftPanel : ViewControl
{
    internal Node Panel0 => GetNode("0");
    internal Node Panel1 => GetNode("1");

    public override void _Ready()
    {
        base._Ready();

        foreach (var panel in Panel1.GetChildren().OfType<LeftContentPanel1>())
        {
            panel.Visible = false;
        }
    }

    internal CityListPanel ShowCityListPanel()
    {
        var panel = CreatePanel0<CityListPanel>();
        panel.ShowCityDetail += (cityListItem) =>
        {
            var task = ShowPanel1Async<CityDetailPanel>();
            task.ContinueWith(x => { x.Result.Id = cityListItem.Id; });
        };

        return panel;
    }

    private T CreatePanel0<T>()
        where T : LeftContentPanel0
    {
        var panel = Panel0.GetChildren().OfType<T>().SingleOrDefault();
        if (panel != null)
        {
            return panel;
        }

        var placeHolder = Panel0.GetNode<InstancePlaceholder>($"{typeof(T).Name}");

        panel = placeHolder.CreateInstance() as T;
        panel.CloseButton.Pressed += () =>
        {
            foreach (var panel in Panel1.GetChildren().OfType<LeftContentPanel1>())
            {
                panel.Visible = false;
            }
        };

        return panel;
    }

    private async Task<T> ShowPanel1Async<T>()
        where T : LeftContentPanel1
    {
        foreach (var panel in Panel1.GetChildren().OfType<LeftContentPanel1>())
        {
            panel.Visible = false;
        }

        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);
        await ToSignal(GetTree(), SceneTree.SignalName.ProcessFrame);

        var resultPanel = Panel1.GetChildren().OfType<T>().Single();
        resultPanel.Visible = true;

        return resultPanel;
    }

}
