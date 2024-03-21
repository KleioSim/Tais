using Tais.Interfaces;

namespace Tais.Sessions;

public static class SessionBuilder
{
    public static ISession Build()
    {
        var session = new Session();

        var cityTaskDefs = Enumerable.Range(0, 3).Select(x => new TaskDef($"CITY_{x}", 5, new CityNameCondition($"CITY_{x}"), new SetCityNotControledOperation())).ToArray();

        var popTaskDefs0 = Enumerable.Range(0, 1).Select(x => new TaskDef($"P0_{x}", 10, new PopMinCountCondition(100), new SetPopCountDec(0.1f))).ToArray();
        var popTaskDefs1 = Enumerable.Range(0, 2).Select(x => new TaskDef($"P1_{x}", 15, new PopMinCountCondition((x + 1) * 500), new SetPopCountDec(0.01f))).ToArray();
        var popTaskDefs2 = Enumerable.Range(0, 3).Select(x => new TaskDef($"P2_{x}", 20, new PopMinCountCondition((x + 1) * 5000), new SetPopCountDec(0.02f))).ToArray();

        for (int i = 0; i < 3; i++)
        {

            var city = new City(cityTaskDefs, $"CITY_{i}",
                true,
                new[]
                {
                    new Pop(popTaskDefs0, "P0", (i+1)*100, true, new Family($"F_{i}", new []{ ("Test", i*10f)})),
                    new Pop(popTaskDefs1, "P1", (i+1)*1000, true, null),
                    new Pop(popTaskDefs2, "P2", (i+1)*10000, false, null)
                });

            session.cities.Add(city);
        }

        session.centralGov.InitTaxValue = session.finance.incomes.Sum(x => x.CurrValue) * 0.8f;




        return session;
    }
}
