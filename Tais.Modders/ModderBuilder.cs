using Tais.Commands;
using Tais.Effects;
using Tais.Modders.Conditions;
using Tais.Modders.Interfaces;

namespace Tais.Modders;

public class ModderBuilder
{
    public static IModder Build()
    {
        var modder = new Modder()
        {
            PopDefs = new List<PopDef>()
            {
                new PopDef()
                {
                    PopName = "HAO",
                    IsRegisted = true,
                    HasFamily = true,
                    TaskDefs = new ITaskDef[] {
                        new TaskDef()
                        {
                            Name = $"HAO_POP_DEC",
                            RequestActionPoint = 2,
                            Speed = 10,
                            Condition = new PopMinCountCondition(100),
                            Command = new Cmd_ChangePopCount(0.1f),
                        },
                        new TaskDef()
                        {
                            Name = $"HAO_VISIT_FAMILY",
                            RequestActionPoint = 3,
                            Speed = 10,
                            Condition = new TrueCondition(),
                            Command = new Cmd_ChangeFamilyAttitude(5.0f),
                        }
                    }
                },
                new PopDef()
                {
                    PopName = "MIN",
                    IsRegisted = true,
                    HasFamily = false,
                    TaskDefs = new ITaskDef[] {
                        new TaskDef()
                        {
                            Name = $"MIN_POP_DEC",
                            RequestActionPoint = 1,
                            Speed = 10,
                            Condition = new PopMinCountCondition(1200),
                            Command = new Cmd_ChangePopCount(0.1f),
                        }
                    }
                },
                new PopDef()
                {
                    PopName = "YIN",
                    IsRegisted = false,
                    HasFamily = false,
                    TaskDefs = new ITaskDef[] {
                        new TaskDef()
                        {
                            Name = $"YIN_POP_DEC",
                            RequestActionPoint = 3,
                            Speed = 10,
                            Condition = new PopMinCountCondition(12000),
                            Command = new Cmd_ChangePopCount(0.1f),
                        }
                    }
                }
            }.ToDictionary(k => k.PopName, v => v as IPopDef),

            CityDef = new CityDef()
            {
                TaskDefs = new ITaskDef[]
                {
                    new TaskDef()
                    {
                        Name = $"SET_CITY0_NO_CONTROLLED",
                        RequestActionPoint = 4,
                        Speed = 5,
                        Condition = new CityNameCondition($"CITY_0"),
                        Command = new Cmd_ChangeCityIsControlFlag(false),
                    },
                    new TaskDef()
                    {
                        Name = $"SET_CITY1_NO_CONTROLLED",
                        RequestActionPoint = 5,
                        Speed = 5,
                        Condition = new CityNameCondition($"CITY_1"),
                        Command = new Cmd_ChangeCityIsControlFlag(false),
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
            },

            CentralGovDef = new CentralGovDef
            {
                EventDefs = new IEventDef[]
                {
                    new EventDef()
                    {
                        Title = "CentralGov RequestTax Not FullFill",
                        Desc = "CentralGov RequestTax Not FullFill Desc",
                        VaildDate = new VaildDate() { Day = 1 },
                        TriggerCondition = new CentralGovRequestTaxNotFullFill(),
                        Opition = new OpitionDef(){ Desc = "Confirm", Command = new Cmd_RevokePlayerTitle() }
                    },
                    new EventDef()
                    {
                        Title = "TrueCondition Test",
                        Desc = "TrueCondition Test Desc",
                        VaildDate = new VaildDate() { Day = 1, Month = 1},
                        TriggerCondition = new TrueCondition(),
                        Opition = new OpitionDef(){ Desc = "Confirm" }
                    },
                },
                WarnDefs = new IWarnDef[]
                {
                    new WarnDef(){Condition = new CentralGovRequestTaxNotFullFill()}
                }
            },

            PlayerDef = new PlayerDef()
        };

        return modder;
    }
};

public class PlayerDef : EntityDef, IPlayerDef
{

}


public class CityDef : EntityDef, ICityDef
{

}

public class BufferDef : IBufferDef
{
    public string BufferName { get; init; }

    public ICondition ValidCondition { get; init; }

    public ICondition InvalidCondition { get; init; }

    public IEnumerable<IEffect> Effects { get; init; }
}

public class EntityDef : IEntityDef
{
    public IEnumerable<ITaskDef> TaskDefs { get; init; } = new List<ITaskDef>();
    public IEnumerable<IBufferDef> BufferDefs { get; init; } = new List<IBufferDef>();
    public IEnumerable<IEventDef> EventDefs { get; init; } = new List<IEventDef>();
    public IEnumerable<IWarnDef> WarnDefs { get; init; } = new List<IWarnDef>();
}

public class WarnDef : IWarnDef
{
    public ICondition Condition { get; init; }
}