using Tais.Commands;
using Tais.Modders.Interfaces;

namespace Tais.Modders.CommandBuilders;

class Cmd_RevokePlayerTitle_Builder : ICommandBuilder
{
    public ICommand Build(object target)
    {
        return new Cmd_RevokePlayerTitle();
    }
}
