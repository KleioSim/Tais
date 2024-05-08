using Tais.Commands;
using Tais.Modders.DataWappers;
using Tais.Modders.Interfaces;

namespace Tais.Modders.CommandBuilders;

public class Cmd_ChangeFamilyAttitude_Builder : ICommandBuilder
{
    private IDataWapper<float> dataWapper;

    public Cmd_ChangeFamilyAttitude_Builder(IDataWapper<float> dataWapper)
    {
        this.dataWapper = dataWapper;
    }

    public ICommand Build(object context)
    {
        return new Cmd_ChangeFamilyAttitude(dataWapper.GetValue(context));
    }
}