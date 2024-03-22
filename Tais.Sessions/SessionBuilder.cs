using System;
using System.Xml.Linq;
using Tais.Interfaces;

namespace Tais.Sessions;

public static class SessionBuilder
{
    public static ISession Build(InitialData initialData)
    {
        var session = new Session();


        var popDefs = new List<PopDef>()
        {
            new PopDef()
            {
                PopName = "HAO",
                IsRegisted = true,
                HasFamily = true,
                TaskDefs = new ITaskDef[]{
                    new TaskDef(
                        $"HAO_POP_VISIT",
                        10,
                        new PopMinCountCondition(100),
                        new SetPopCountDec(0.1f)
                    )
                }
            },
            new PopDef()
            {
                PopName = "MIN",
                IsRegisted = true,
                HasFamily = true,
                TaskDefs = new ITaskDef[]{
                    new TaskDef(
                        $"MIN_POP_VISIT",
                        10,
                        new PopMinCountCondition(1200),
                        new SetPopCountDec(0.1f)
                    )
                }
            },
            new PopDef()
            {
                PopName = "YIN",
                IsRegisted = true,
                HasFamily = true,
                TaskDefs = new ITaskDef[]{
                    new TaskDef(
                        $"YIN_POP_VISIT",
                        10,
                        new PopMinCountCondition(12000),
                        new SetPopCountDec(0.1f)
                    )
                }
            }
        }.ToDictionary(k => k.PopName, v => v);


        var cityDef = new CityDef()
        {
            TaskDefs = new ITaskDef[]
            {
                new TaskDef(
                    $"SET_CITY0_NO_CONTROLLED",
                    5,
                    new CityNameCondition($"CITY_0"),
                    new SetCityNotControledOperation()
                ),
                new TaskDef(
                    $"SET_CITY1_NO_CONTROLLED",
                    5,
                    new CityNameCondition($"CITY_1"),
                    new SetCityNotControledOperation()
                )
            }
        };

        foreach (var cityInitData in initialData.CityInitDatas)
        {
            var pops = initialData.City2PopInitDatas[cityInitData.CityName].Select(popInit => new Pop(popDefs[popInit.PopName], popInit));
            var city = new City(cityDef, cityInitData, pops);

            session.cities.Add(city);
        }

        session.centralGov.InitTaxValue = session.finance.incomes.Sum(x => x.CurrValue) * 0.8f;


        return session;
    }
}

public class InitialData
{
    public IEnumerable<ICityInitData> CityInitDatas { get; init; }

    public Dictionary<string, IEnumerable<IPopInitData>> City2PopInitDatas { get; init; }
}

public class CityInitData : ICityInitData
{
    public string CityName { get; init; }

    public bool IsControlled { get; init; }
}

public class CityDef : ICityDef
{
    public IEnumerable<ITaskDef> TaskDefs { get; init; }
}