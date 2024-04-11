using Godot;
using System.Collections.Generic;
using System.Linq;
using Tais.Commands;
using Tais.Interfaces;

public partial class MainScenePresent : PresentControl<MainScene, ISession>
{
    public override void _EnterTree()
    {
        base._EnterTree();

        if (!IsMock)
        {
            var global = GetNode<Global>("/root/Global");
            Model = global.session;
        }
    }

    protected override void Initialize(MainScene view, ISession model)
    {
        view.CreateTask.Pressed += () =>
        {
            SendCommand(new Cmd_CreateTask { });
        };

        view.NextTurn.Pressed += () =>
        {
            view.EmitSignal(MainScene.SignalName.NextTurnStarted);

            model.Toasts.Clear();

            view.NextDayTimer.Start();
        };

        view.NextDayTimer.Timeout += () =>
        {
            StartCoroutine(view, model);
        };
    }

    public async void StartCoroutine(MainScene view, ISession session)
    {
        view.NextDayTimer.Stop();

        foreach (var eventObj in session.OnDaysInc())
        {
            PresentBase.SendCommand(new Cmd_UIRefresh());

            var dialog = view.EventDialogHolder.CreateInstance() as EventDialog;
            dialog.Object = eventObj;
            dialog.Visible = true;

            await ToSignal(dialog, EventDialog.SignalName.TreeExited);

            if (Model.Player.IsRevoked || Model.Player.IsDead)
            {
                View.GMFailedEvent.Visible = true;
                return;
            }
        }

        PresentBase.SendCommand(new Cmd_UIRefresh());

        if (session.Date.Day == 1 || session.Date.Day == 15)
        {
            view.EmitSignal(MainScene.SignalName.NextTurnFinished);
        }
        else
        {
            view.NextDayTimer.Start();
        }
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

        view.FreeActionPoint.Text = model.Player.FreeActionPoints.ToString();
        view.TotalActionPoint.Text = model.Player.TotalActionPoints.ToString();

        view.TaskContainer.Refresh(model.Tasks.OfType<object>().ToHashSet());
        view.ToastContainer.Refresh(model.Toasts.OfType<object>().ToHashSet());

        if (model.CurrEvent != null)
        {
            var dialog = view.EventDialogHolder.CreateInstance() as EventDialog;
            dialog.Object = model.CurrEvent;
        }
    }
}