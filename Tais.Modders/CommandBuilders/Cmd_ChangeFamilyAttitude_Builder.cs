using Tais.Commands;
using Tais.Modders.DataWappers;
using Tais.Modders.Interfaces;

namespace Tais.Modders.CommandBuilders;

class Cmd_ChangeFamilyAttitude_Builder : ICommandBuilder
{
    private IDataWapper<float> dataWapper;

    public Cmd_ChangeFamilyAttitude_Builder(IDataWapper<float> dataWapper)
    {
        this.dataWapper = dataWapper;
    }

    public ICommand Build(object target)
    {
        return new Cmd_ChangeFamilyAttitude(dataWapper.GetValue(target));
    }
}