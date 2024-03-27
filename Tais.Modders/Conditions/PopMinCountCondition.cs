using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Modders.Conditions;

public class PopMinCountCondition : ICondition
{
    private int minCount;

    public PopMinCountCondition(int minCount)
    {
        this.minCount = minCount;
    }

    public bool IsSatisfied(object obj)
    {
        return (int)((IPop)obj).Count >= minCount;
    }

    public override string ToString()
    {
        return $"PopCount more or equal {minCount}";
    }
}
