using Tais.Effects;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.CentralGovs;

class RequestTax : IRequestTax
{
    public float BaseValue => centralGov.InitTaxValue;

    public IEnumerable<IEffect> Effects => centralGov.effects.Where(x => x is CentralRequestTaxEffect);

    private CentralGov centralGov;

    public RequestTax(CentralGov centralGov)
    {
        this.centralGov = centralGov;
    }
}