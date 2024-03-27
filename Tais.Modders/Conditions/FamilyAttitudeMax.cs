using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Modders.Conditions;

public class FamilyAttitudeMax : ICondition
{
    private float maxValue;

    public FamilyAttitudeMax(float maxValue)
    {
        this.maxValue = maxValue;
    }

    public bool IsSatisfied(object target)
    {
        var city = target as ICity;
        var pop = city.Pops.Single(x => x.Family != null);

        return pop.Family.Attitude.Current < maxValue;
    }
}
