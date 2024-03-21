using System.Linq;
using Tais.Interfaces;

public partial class CityOperationListPresent : PresentControl<CityOperationList, ISession>
{
    protected override void Initialize(CityOperationList view, ISession model)
    {
    }

    protected override void Update(CityOperationList view, ISession model)
    {
        var city = view.GetTarget() as ICity;

        view.OperationContainer.Refresh(city.TaskDefs.Select(x => (object)x).ToHashSet());
    }
}