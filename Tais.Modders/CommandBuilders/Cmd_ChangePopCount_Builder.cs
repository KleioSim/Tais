using Tais.Commands;
using Tais.Modders.DataWappers;
using Tais.Modders.Interfaces;

namespace Tais.Modders.CommandBuilders;

class Cmd_ChangePopCount_Builder : ICommandBuilder
{
    private IDataWapper<float> dataWapper;

    public Cmd_ChangePopCount_Builder(IDataWapper<float> dataWapper)
    {
        this.dataWapper = dataWapper;
    }

    public ICommand Build(object target)
    {
        return new Cmd_ChangePopCount(dataWapper.GetValue(target));
    }
}