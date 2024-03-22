﻿using System;
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
                        $"HAO_POP_DEC",
                        10,
                        new PopMinCountCondition(100),
                        new SetPopCountDec(0.1f)
                    ),
                    new TaskDef(
                        $"HAO_VISIT_FAMILY",
                        10,
                        new TrueCondition(),
                        new ChangeFamilyAttitude(10f)
                    )
                }
            },
            new PopDef()
            {
                PopName = "MIN",
                IsRegisted = true,
                HasFamily = false,
                TaskDefs = new ITaskDef[]{
                    new TaskDef(
                        $"MIN_POP_DEC",
                        10,
                        new PopMinCountCondition(1200),
                        new SetPopCountDec(0.1f)
                    )
                }
            },
            new PopDef()
            {
                PopName = "YIN",
                IsRegisted = false,
                HasFamily = false,
                TaskDefs = new ITaskDef[]{
                    new TaskDef(
                        $"YIN_POP_DEC",
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
            },
            BufferDefs = new IBufferDef[]
            {
                new BufferDef()
                {
                    BufferName = "HAO_ATTITUDE_HELP",
                    ValidCondition = new FamilyAttitudeMin(20),
                    InvalidCondition = new FamilyAttitudeMax(10),
                    Effects = new IEffect[]
                    {
                        new PopTaxChangePercentEffect(0.15f)
                    }
                }
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
    public IEnumerable<IBufferDef> BufferDefs { get; init; }
}

public class BufferDef : IBufferDef
{
    public string BufferName { get; init; }

    public ICondition ValidCondition { get; init; }

    public ICondition InvalidCondition { get; init; }

    public IEnumerable<IEffect> Effects { get; init; }
}