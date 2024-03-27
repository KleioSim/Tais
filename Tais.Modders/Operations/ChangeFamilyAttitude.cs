using Tais.Modders.Interfaces;
using Tais.Pops;

namespace Tais.Modders.Operations;

public class ChangeFamilyAttitude : IOperation
{
    private float value;

    public ChangeFamilyAttitude(float value)
    {
        this.value = value;
    }

    public void Do(string desc, object target)
    {
        var pop = target as Pop;

        var attitude = pop.Family.Attitude as Attitude;
        attitude.items.Add(new Attitude.Item(desc, value));
    }

    public override string ToString()
    {
        return $"FamilyAttitude Change {(value < 0 ? "-" : "+")}{value}";
    }
}