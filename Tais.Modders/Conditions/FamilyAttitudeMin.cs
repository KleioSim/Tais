using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Modders.Conditions;

public class FamilyAttitudeMin : ICondition
{
    private float minValue;

    public FamilyAttitudeMin(float minValue)
    {
        this.minValue = minValue;
    }

    public bool IsSatisfied(IProcessContext context)
    {
        if (((ProcessContext)context).current is not ICity city)
        {
            throw new Exception();
        }

        var pop = city.Pops.Single(x => x.Family != null);

        return pop.Family.Attitude.Current > minValue;
    }
}
