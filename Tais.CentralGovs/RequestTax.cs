using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.CentralGovs;

class RequestTax : IRequestTax
{
    public float BaseValue => centralGov.InitTaxValue;

    public IEnumerable<IEffect> Effects => effects;

    private List<IEffect> effects = new List<IEffect>();

    private CentralGov centralGov;

    public RequestTax(CentralGov centralGov)
    {
        this.centralGov = centralGov;
    }
}