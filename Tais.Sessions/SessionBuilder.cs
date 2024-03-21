﻿using System.Xml.Linq;
using Tais.Interfaces;

namespace Tais.Sessions;

public static class SessionBuilder
{
    public static ISession Build()
    {
        var session = new Session();

        var cityTaskDefs = Enumerable.Range(0, 3).Select(x => new TaskDef($"CITY_{x}", 5, new CityNameCondition($"CITY_{x}"), new SetCityNotControledOperation())).ToArray();

        //var popTaskDefs0 = Enumerable.Range(0, 1).Select(x => new TaskDef($"P0_{x}", 10, new PopMinCountCondition(100), new SetPopCountDec(0.1f))).ToArray();
        //var popTaskDefs1 = Enumerable.Range(0, 2).Select(x => new TaskDef($"P1_{x}", 15, new PopMinCountCondition((x + 1) * 500), new SetPopCountDec(0.01f))).ToArray();
        //var popTaskDefs2 = Enumerable.Range(0, 3).Select(x => new TaskDef($"P2_{x}", 20, new PopMinCountCondition((x + 1) * 5000), new SetPopCountDec(0.02f))).ToArray();

        var popDefs = Enumerable.Range(0, 3).Select(x =>
        {
            var index = x + 1;
            return new PopDef()
            {
                Name = $"POP{x}",
                IsRegisted = x != 2,
                HasFamily = x == 0,
                TaskDefs = Enumerable.Range(0, index)
                    .Select(y => new TaskDef($"POP{x}_{y}",
                                            index * 5,
                                            new PopMinCountCondition(index * (int)Math.Pow(10, x + 1) * 10),
                                            new SetPopCountDec(index * (float)Math.Pow(0.1, x + 1))))
                    .ToArray()
            };
        }).ToArray();

        var initDatas = Enumerable.Range(0, 3).Select(x => new PopInitData()
        {
            Count = (int)Math.Pow(10, x + 1) * 10,

        }).ToArray();

        for (int i = 0; i < 3; i++)
        {

            var city = new City(cityTaskDefs, $"CITY_{i}",
                true,
                new[]
                {
                    new Pop(popDefs[0], initDatas[0]),
                    new Pop(popDefs[1], initDatas[1]),
                    new Pop(popDefs[2], initDatas[2])
                });

            session.cities.Add(city);
        }

        session.centralGov.InitTaxValue = session.finance.incomes.Sum(x => x.CurrValue) * 0.8f;




        return session;
    }
}
