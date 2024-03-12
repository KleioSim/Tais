using System;
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
    }
}