﻿using Microsoft.VisualBasic;
using Tais.Effects;
using Tais.Entities;
using Tais.InitialDatas.Interfaces;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.CentralGovs;

class CentralGov : Entity, ICentralGov
{
    public IRequestTax RequestTax => requestTax;

    public float InitTaxValue { get; internal set; }

    public RequestTax requestTax;

    public ICentralGovDef def { get; set; }

    public TaxLevel TaxLevel { get; internal set; }

    internal IEnumerable<IEffect> effects => GetTaxLevelEffect(TaxLevel);

    public CentralGov()
    {
        requestTax = new RequestTax(this);
    }

    internal void OnDaysInc(IDate date)
    {

    }

    internal void Initialize(ICentralGovDef def, ICentralGovInitData centralGovInitData)
    {
        this.def = def;
        this.TaxLevel = Enum.Parse<TaxLevel>(centralGovInitData.TaxLevel);
    }

    private IEnumerable<IEffect> GetTaxLevelEffect(TaxLevel taxLevel)
    {
        switch (taxLevel)
        {
            case TaxLevel.VLOW:
                return new IEffect[] { new CentralRequestTaxEffect("curr centralGov tax level", -0.4f) };
            case TaxLevel.LOW:
                return new IEffect[] { new CentralRequestTaxEffect("curr centralGov tax level", -0.2f) };
            case TaxLevel.MID:
                return new IEffect[] { new CentralRequestTaxEffect("curr centralGov tax level", 0.0f) };
            case TaxLevel.HIGH:
                return new IEffect[] { new CentralRequestTaxEffect("curr centralGov tax level", 0.3f) };
            case TaxLevel.VHIGH:
                return new IEffect[] { new CentralRequestTaxEffect("curr centralGov tax level", 0.9f) };
            default:
                throw new Exception();
        }
    }
}