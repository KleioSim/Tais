using Tais.Interfaces;

namespace Tais.Sessions;

public static class SessionBuilder
{
    public static ISession Build()
    {
        var session = new Session();

        for (int i = 0; i < 3; i++)
        {

            var city = new City($"CITY_{i}",
                true,
                new[]
                {
                    new Pop("P0", (i+1)*100, true),
                    new Pop("P1", (i+1)*1000, true),
                    new Pop("P2", (i+1)*10000, false)
                });

            session.cities.Add(city);
        }

        session.centralGov.InitTaxValue = session.finance.incomes.Sum(x => x.CurrValue) * 0.8f;

        return session;
    }
}
