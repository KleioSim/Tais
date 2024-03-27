using Tais.Commands;
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
        CommandSender.Send(new Cmd_ChangeFamilyAttitude() { Reason = desc, Target = target, Value = value });
    }

    public override string ToString()
    {
        return $"FamilyAttitude Change {(value < 0 ? "-" : "+")}{value}";
    }
}