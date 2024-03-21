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
            var itemObj = ((ITaskDef def, object target))view.Id;

            SendCommand(new Cmd_TaskStart(itemObj.def, itemObj.target));
        };
    }

    protected override void Update(CityOperationItem view, ISession model)
    {
        var itemObj = ((ITaskDef def, object target))view.Id;

        view.Button.Text = itemObj.def.Name;
        view.Button.Disabled = model.Tasks.Any(x => x.Target == itemObj.target && x.Def == itemObj.def)
            || !itemObj.def.Condition.IsSatisfied(itemObj.target);
    }
}
