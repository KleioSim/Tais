using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Modders.Conditions;

public class TrueCondition : ICondition
{
    public bool IsSatisfied(IProcessContext context)
    {
        return true;
    }
}