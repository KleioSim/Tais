using Godot;
using System.Linq;
using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

public partial class CityOperationItemPresent : PresentControl<CityOperationItem, ISession>
{
    protected override void Initialize(CityOperationItem view, ISession model)
    {
        view.Button.Pressed += () =>
        {
            var itemObj = ((ITaskDef def, object target))view.Id;

            SendCommand(new Cmd_TaskStart(itemObj.def, itemObj.target));
        };

        view.TooltipTrigger.funcGetToolTipString = () =>
        {
            var itemObj = ((ITaskDef def, object target))view.Id;

            var desc = itemObj.def.Command.Desc + "\n";

            desc += "\n";
            desc += $"Request Player Engine {itemObj.def.RequestActionPoint}" + "\n";

            if (!itemObj.def.Condition.IsSatisfied(itemObj.target))
            {
                desc += "\n" + itemObj.def.Condition.ToString();
            }

            return desc;
        };
    }

    protected override void Update(CityOperationItem view, ISession model)
    {
        var itemObj = ((ITaskDef def, object target))view.Id;

        view.Button.Text = itemObj.def.Name;
        view.Button.Disabled = model.Tasks.Any(x => x.Target == itemObj.target && x.Def == itemObj.def)
            || itemObj.def.RequestActionPoint > model.Player.FreeActionPoints
            || !itemObj.def.Condition.IsSatisfied(itemObj.target);
    }
}
