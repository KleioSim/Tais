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
            SendCommand(new Cmd_NextTurn());

            view.NextTurn.Disabled = true;
            view.NextDayTimer.Start();
        };

        view.NextDayTimer.Timeout += () =>
        {
            SendCommand(new Cmd_NextDay());

            if (model.Date.Day == 1 || model.Date.Day == 16)
            {
                view.NextTurn.Disabled = false;
                view.NextDayTimer.Stop();
            }
        };
    }

    protected override void Update(MainScene view, ISession model)
    {
        view.FinanceCurrent.Text = model.Finance.Current.ToString();
        view.FinanceSurplus.Text = model.Finance.Surplus.ToString();
        view.CityCount.Text = model.Cities.Count(x => x.IsOwned).ToString();
        view.PopCount.Text = model.Cities.Where(x => x.IsOwned).Sum(x => x.PopCount.Current).ToString();

        view.Year.Text = model.Date.Year.ToString();
        view.Month.Text = model.Date.Month.ToString();
        view.Day.Text = model.Date.Day.ToString();

        view.TaskContainer.Refresh(model.Tasks.OfType<object>().ToHashSet());
        view.ToastContainer.Refresh(model.Toasts.OfType<object>().ToHashSet());

        if (model.CurrEvent != null)
        {
            var dialog = view.EventDialogHolder.CreateInstance() as EventDialog;
            dialog.Object = model.CurrEvent;
        }
    }
}