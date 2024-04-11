using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface ICentralGov : IEntity
{
    IRequestTax RequestTax { get; }
}

public interface IRequestTax : IEffectValue
{

}