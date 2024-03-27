using Tais.Modders.Interfaces;

namespace Tais.Modders.Conditions;

public class TrueCondition : ICondition
{
    public bool IsSatisfied(object target)
    {
        return true;
    }
}