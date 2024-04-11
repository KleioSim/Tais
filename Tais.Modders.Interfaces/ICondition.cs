using System.Security.Principal;

namespace Tais.Modders.Interfaces;

public interface ICondition
{
    bool IsSatisfied(IProcessContext context);
}

public interface IProcessContext
{

}