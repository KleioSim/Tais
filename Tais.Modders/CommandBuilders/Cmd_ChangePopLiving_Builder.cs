using Tais.Commands;
using Tais.Modders.DataWappers;
using Tais.Modders.Interfaces;

namespace Tais.Modders.CommandBuilders;

public class Cmd_ChangePopLiving_Builder : ICommandBuilder
{
    private IDataWapper<float> dataWapper;

    public Cmd_ChangePopLiving_Builder(IDataWapper<float> dataWapper)
    {
        this.dataWapper = dataWapper;
    }

    public ICommand Build(object target)
    {
        return new Cmd_ChangePopLiving(dataWapper.GetValue(target));
    }
}