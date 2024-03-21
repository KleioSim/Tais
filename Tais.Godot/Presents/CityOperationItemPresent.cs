using Godot;
using System.Linq;
using Tais.Commands;
using Tais.Interfaces;

public partial class CityOperationItemPresent : PresentControl<CityOperationItem, ISession>
{
    protected override void Initialize(CityOperationItem view, ISession model)
    {
        view.Button.Pressed += () =>
        {
            SendCommand(new Cmd_TaskStart(view.Id, view.GetTarget()));
        };
    }

    protected override void Update(CityOperationItem view, ISession model)
    {
        var def = view.Id as ITaskDef;

        view.Button.Text = def.Name;
        view.Button.Disabled = model.Tasks.Any(x => x.Target == view.GetTarget() && x.Def == def)
            || !def.Condition.IsSatisfied(view.GetTarget() as ICity);
    }
}
