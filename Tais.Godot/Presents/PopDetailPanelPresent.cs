using System;
using System.Linq;
using Tais.Interfaces;

public partial class PopDetailPanelPresent : PresentControl<PopDetailPanel, ISession>
{
    protected override void Initialize(PopDetailPanel view, ISession model)
    {

    }

    protected override void Update(PopDetailPanel view, ISession model)
    {
        var func = view.Id as Func<IPop>;
        var pop = func.Invoke();

        view.PopName.Text = pop.Name;

        view.PopCount.Text = pop.Count.ToString();

        if (!pop.IsRegisted)
        {
            view.PopCount.Text = string.Join("", Enumerable.Range(0, view.PopCount.Text.Length).Select(_ => "*"));
        }

        if (pop.Family != null)
        {
            view.FamilyName.Text = pop.Family.Name;

            view.AttitudePanel.Visible = true;
            view.Attitude.Text = pop.Family.Attitude.Current.ToString();
        }
        else
        {
            view.FamilyName.Text = "";
            view.AttitudePanel.Visible = false;
        }

    }
}