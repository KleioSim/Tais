using Tais.Modders.Interfaces;
using Tais.Pops;

namespace Tais.Modders.Operations;

public class SetPopCountDec : IOperation
{
    private float percent;

    public SetPopCountDec(float percent)
    {
        this.percent = percent;
    }

    public void Do(string desc, object target)
    {
        var pop = target as Pop;
        pop.Count -= pop.Count * percent;
    }

    public override string ToString()
    {
        return $"SetPopCountDec -{percent * 100}%";
    }
}
