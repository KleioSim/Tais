using Tais.Commands;
using Tais.Modders.DataWappers;
using Tais.Modders.Interfaces;

namespace Tais.Modders.CommandBuilders;

public class Cmd_ChangeFinance_Builder : ICommandBuilder
{
    private IDataWapper<float> dataWapper;

    public Cmd_ChangeFinance_Builder(IDataWapper<float> dataWapper)
    {
        this.dataWapper = dataWapper;
    }

    public ICommand Build(object context)
    {
        return new Cmd_ChangeFinance(dataWapper.GetValue(context));
    }
}