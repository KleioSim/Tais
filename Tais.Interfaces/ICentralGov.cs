using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface ICentralGov : IEntity
{
    TaxLevel TaxLevel { get; }
    int ReportPopCount { get; }
    IRequestTax RequestTax { get; }
}

public interface IRequestTax : IEffectValue
{

}

public enum TaxLevel
{
    VLOW,
    LOW,
    MID,
    HIGH,
    VHIGH
}