using Godot;
using Tais.Interfaces;

public partial class CityOperationItemPresent : PresentControl<CityOperationItem, ISession>
{
    protected override void Initialize(CityOperationItem view, ISession model)
    {
        view.Button.Pressed += () =>
        {
            var def = view.Id as ICityTaskDef;
            GD.Print($"Start Task {def.Name}");
        };
    }

    protected override void Update(CityOperationItem view, ISession model)
    {
        var def = view.Id as ICityTaskDef;

        view.Button.Text = def.Name;
        view.Button.Disabled = !def.Condition.IsSatisfied(view.GetTarget() as ICity);
    }
}
