using System.Linq;
using Tais.Interfaces;

public partial class CityDetailPanelPresent : PresentControl<CityDetailPanel, ISession>
{
    protected override void Initialize(CityDetailPanel view, ISession model)
    {

    }

    protected override void Update(CityDetailPanel view, ISession model)
    {
        var cityObj = view.Id as ICity;

        view.CityName.Text = cityObj.Name;
        view.PopCount.Text = cityObj.PopCount.Current.ToString();
        view.PopTax.Text = cityObj.PopTax.CurrValue.ToString();

        view.PopContainer.Refresh(cityObj.Pops.Select(x => (object)x).ToHashSet());
    }
}

public partial class PopItemPresent : PresentControl<PopItem, ISession>
{
    protected override void Initialize(PopItem view, ISession model)
    {

    }

    protected override void Update(PopItem view, ISession model)
    {
        var popObj = view.Id as IPop;

        view.PopName.Text = popObj.Name;

        view.PopCount.Text = popObj.Count.ToString();
        if (!popObj.IsRegisted)
        {
            view.PopCount.Text = string.Join("", Enumerable.Range(0, view.PopCount.Text.Length).Select(_ => "*"));
        }

    }
}