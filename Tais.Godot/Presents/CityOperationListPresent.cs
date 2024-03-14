using System.Linq;
using Tais.Interfaces;

public partial class CityOperationListPresent : PresentControl<CityOperationList, ISession>
{
    protected override void Initialize(CityOperationList view, ISession model)
    {
    }

    protected override void Update(CityOperationList view, ISession model)
    {
        view.OperationContainer.Refresh(model.CityTaskDefs.Select(x => (object)x).ToHashSet());
    }
}