using Tais.Commands;
using Tais.Modders.Interfaces;

namespace Tais.Modders.CommandBuilders;

public class Cmd_ChangeCityIsControlFlag_Builder : ICommandBuilder
{
    private bool flag;

    public Cmd_ChangeCityIsControlFlag_Builder(bool flag)
    {
        this.flag = flag;
    }

    public ICommand Build(object target)
    {
        return new Cmd_ChangeCityIsControlFlag(flag);
    }
}