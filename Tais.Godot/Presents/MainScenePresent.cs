using System.Linq;
using Tais.Commands;
using Tais.Interfaces;

public partial class MainScenePresent : PresentControl<MainScene, ISession>
{
    protected override void Initialize(MainScene view, ISession model)
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


    protected override void Update(MainScene view, ISession model)
    {
        view.FinanceCurrent.Text = model.Finance.Current.ToString();
        view.FinanceSurplus.Text = model.Finance.Surplus.ToString();
        view.CityCount.Text = model.Cities.Count(x => x.IsOwned).ToString();
        view.PopCount.Text = model.Cities.Where(x => x.IsOwned).Sum(x => x.PopCount.Current).ToString();

        view.TaskContainer.Refresh(model.Tasks.OfType<object>().ToHashSet());

        if (model.CurrEvent != null)
        {
            var dialog = view.EventDialogHolder.CreateInstance() as EventDialog;
            dialog.Object = model.CurrEvent;
        }

        view.NextTurn.Disabled = model.CurrEvent != null;
    }
}