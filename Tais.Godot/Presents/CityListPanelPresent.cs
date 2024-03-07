using System.Linq;
using Tais.Commands;
using Tais.Interfaces;

public partial class CityListPanelPresent : PresentControl<CityListPanel, ISession>
{
    protected override void Initialize(CityListPanel view, ISession model)
    {

    }


    protected override void Update(CityListPanel view, ISession model)
    {
        view.ItemContainer.Refresh(model.Cities.OfType<object>().ToHashSet());
    }
}
