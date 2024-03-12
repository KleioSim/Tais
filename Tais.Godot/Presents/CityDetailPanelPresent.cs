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
