using Tais.Commands;

namespace Tais.Modders.Interfaces;

public interface ICommandBuilder
{
    ICommand Build(object target);
}