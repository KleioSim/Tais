using Tais.Interfaces;

namespace Tais.Sessions;

public static class SessionBuilder
{
    public static ISession Build()
    {
        var session = new Session();

        session.cities.AddRange(new[] { new City(10000, true), new City(1000, true), new City(100000, true) });
        session.centralGov.InitTaxValue = session.finance.incomes.Sum(x => x.CurrValue) * 0.8f;

        return session;
    }
}
