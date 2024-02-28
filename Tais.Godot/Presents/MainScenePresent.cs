using System.Linq;
using Tais.Commands;
using Tais.Interfaces;

public partial class MainScenePresent : PresentControl<MainScene, ISessionProxy>
{
    protected override void Initialize(MainScene view, ISessionProxy model)
    {
        view.CreateTask.Pressed += () =>
        {
            SendCommand(new Cmd_CreateTask { });
        };

        view.NextTurn.Pressed += () =>
        {
            SendCommand(new Cmd_NextTurn { });
        };
    }


    protected override void Update(MainScene view, ISessionProxy model)
    {
        view.FinanceCurrent.Text = model.Finance.Current.ToString();
        view.FinanceSurplus.Text = model.Finance.Surplus.ToString();

        view.TaskContainer.Refresh(model.Tasks.OfType<object>().ToHashSet());

        if (model.CurrEvent != null)
        {
            var dialog = view.EventDialogHolder.CreateInstance() as EventDialog;
            dialog.Object = model.CurrEvent;
        }

        view.NextTurn.Disabled = model.CurrEvent != null;
    }
}