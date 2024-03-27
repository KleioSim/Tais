using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface ICentralGov
{
    IRequestTax RequestTax { get; }
}

public interface IRequestTax : IEffectValue
{

}