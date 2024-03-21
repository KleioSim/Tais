using Tais.Interfaces;

namespace Tais.Sessions;

public static class SessionBuilder
{
    public static ISession Build()
    {
        var session = new Session();

        var cityTaskDefs = Enumerable.Range(0, 3).Select(x => new CityTaskDef($"CITY_{x}", 5, new CityNameCondition($"CITY_{x}"), new SetCityNotControledOperation())).ToArray();

        for (int i = 0; i < 3; i++)
        {

            var city = new City(cityTaskDefs, $"CITY_{i}",
                true,
                new[]
                {
                    new Pop("P0", (i+1)*100, true, new Family($"F_{i}", new []{ ("Test", i*10f)})),
                    new Pop("P1", (i+1)*1000, true, null),
                    new Pop("P2", (i+1)*10000, false, null)
                });

            session.cities.Add(city);
        }

        session.centralGov.InitTaxValue = session.finance.incomes.Sum(x => x.CurrValue) * 0.8f;




        return session;
    }
}
