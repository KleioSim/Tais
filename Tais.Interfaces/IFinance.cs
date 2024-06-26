﻿using Tais.Modders.Interfaces;

namespace Tais.Interfaces;

public interface IFinance
{
    float Current { get; }
    float Surplus { get; }
    float ExpectYearReserve { get; }

    IEnumerable<IEffectValue> Incomes { get; }
    IEnumerable<IEffectValue> Spends { get; }
}