using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Modders.Conditions;

public class FamilyAttitudeMin : ICondition
{
    private float minValue;

    public FamilyAttitudeMin(float minValue)
    {
        this.minValue = minValue;
    }

    public bool IsSatisfied(object target)
    {
        var city = target as ICity;
        var pop = city.Pops.Single(x => x.Family != null);

        return pop.Family.Attitude.Current > minValue;
    }
}
