using Godot;
using System;
using System.Linq;
using Tais.Commands;
using Tais.Godot.Utilities.UBBCodes;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

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

            var def = itemObj.def;

            var desc = String.Join("\n", def.CommandBuilders.Select(x => x.Build(itemObj.target).Desc));

            desc += "\n";
            desc += UBB.Core($"Request ActionPoint {def.RequestActionPoint}")
                .Color(def.RequestActionPoint < model.Player.FreeActionPoints ? UBBColor.GREEN : UBBColor.RED) + "\n";

            desc += "\n" + itemObj.def.Condition.ToString();

            return desc;
        };
    }

    protected override void Update(CityOperationItem view, ISession model)
    {
        var itemObj = ((ITaskDef def, object target))view.Id;

        var context = new ProcessContext() { session = model, current = itemObj.target as IEntity };

        view.Button.Text = itemObj.def.Name;
        view.Button.Disabled = model.Tasks.Any(x => x.Target == itemObj.target && x.Def == itemObj.def)
            || itemObj.def.RequestActionPoint > model.Player.FreeActionPoints
            || !itemObj.def.Condition.IsSatisfied(context);
    }
}