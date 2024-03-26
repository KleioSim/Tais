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
                    new TaskDef()
                    {
                        Name = $"HAO_POP_DEC",
                        RequestActionPoint = 2,
                        Speed = 10,
                        Condition = new PopMinCountCondition(100),
                        Operation = new SetPopCountDec(0.1f)
                    },
                    new TaskDef()
                    {
                        Name = $"HAO_VISIT_FAMILY",
                        RequestActionPoint = 3,
                        Speed = 10,
                        Condition = new TrueCondition(),
                        Operation = new ChangeFamilyAttitude(5.0f)
                    }
                }
            },
            new PopDef()
            {
                PopName = "MIN",
                IsRegisted = true,
                HasFamily = false,
                TaskDefs = new ITaskDef[]{
                    new TaskDef()
                    {
                        Name = $"MIN_POP_DEC",
                        RequestActionPoint = 1,
                        Speed = 10,
                        Condition = new PopMinCountCondition(1200),
                        Operation = new SetPopCountDec(0.1f)
                    }
                }
            },
            new PopDef()
            {
                PopName = "YIN",
                IsRegisted = false,
                HasFamily = false,
                TaskDefs = new ITaskDef[]{
                    new TaskDef()
                    {
                        Name = $"YIN_POP_DEC",
                        RequestActionPoint = 3,
                        Speed = 10,
                        Condition = new PopMinCountCondition(12000),
                        Operation = new SetPopCountDec(0.1f)
                    }
                }
            }
        }.ToDictionary(k => k.PopName, v => v);


        var cityDef = new CityDef()
        {
            TaskDefs = new ITaskDef[]
            {
                new TaskDef()
                {
                    Name = $"SET_CITY0_NO_CONTROLLED",
                    RequestActionPoint = 4,
                    Speed = 5,
                    Condition = new CityNameCondition($"CITY_0"),
                    Operation = new SetCityNotControledOperation()
                },
                new TaskDef()
                {
                    Name = $"SET_CITY1_NO_CONTROLLED",
                    RequestActionPoint = 5,
                    Speed = 5,
                    Condition = new CityNameCondition($"CITY_1"),
                    Operation = new SetCityNotControledOperation()
                }
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
                        new PopTaxChangePercentEffect("HAO_ATTITUDE_HELP", 0.15f)
                    }
                }
            }
        };

        session.player.Initialize(initialData.PlayerInitData);

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
    public IPlayerInitData PlayerInitData { get; init; }
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