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

        var itemObjs = city.TaskDefs.Select(x => (object)(x, (object)city))
            .Concat(city.Pops.Select(pop => pop.TaskDefs.Select(def => (object)(def, (object)pop)))
            .SelectMany(x => x))
        .ToHashSet();

        view.OperationContainer.Refresh(itemObjs);
    }
}