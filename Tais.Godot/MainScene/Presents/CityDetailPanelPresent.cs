using Godot;
using System.Linq;
using Tais.Interfaces;

public partial class CityDetailPanelPresent : PresentControl<CityDetailPanel, ISession>
{
    protected override void Initialize(CityDetailPanel view, ISession model)
    {
        view.PopContainer.OnAddedItem = (item) =>
        {
            item.Button.Pressed += () =>
            {
                view.PopDetailPanel.Visible = true;
                view.PopDetailPanel.Id = () =>
                {
                    var cityObj = view.Id as ICity;
                    var popObj = item.Id as IPop;
                    return cityObj.Pops.SingleOrDefault(x => x.Name == popObj.Name);
                };
            };
        };

        view.PopTaxToolTipTrigger.funcGetToolTipString = () =>
        {
            var cityObj = view.Id as ICity;

            return $"BaseValue  {cityObj.PopTax.BaseValue}" + "\n" + string.Join("\n", cityObj.PopTax.Effects.Select(x => $"{x.Desc}  {x.Percent.ToString("+0%;-#%")}"));
        };
    }

    protected override void Update(CityDetailPanel view, ISession model)
    {
        var cityObj = view.Id as ICity;

        view.DebugID.Text = cityObj.Id;
        view.CityName.Text = cityObj.Name;
        view.PopCount.Text = cityObj.RegistPopCount.Current.ToString();
        view.PopTax.Text = cityObj.PopTax.CurrValue.ToString();

        view.PopContainer.Refresh(cityObj.Pops.Select(x => (object)x).ToHashSet());
        view.BufferContainer.Refresh(cityObj.Buffers.Select(x => (object)x).ToHashSet());
    }
}
