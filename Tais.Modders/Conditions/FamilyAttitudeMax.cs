using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Modders.Conditions;

public class FamilyAttitudeMax : ICondition
{
    private float maxValue;

    public FamilyAttitudeMax(float maxValue)
    {
        this.maxValue = maxValue;
    }


    public bool IsSatisfied(IProcessContext context)
    {
        if (((ProcessContext)context).current is not ICity city)
        {
            throw new Exception();
        }

        var pop = city.Pops.Single(x => x.Family != null);

        return pop.Family.Attitude.Current < maxValue;
    }
}
