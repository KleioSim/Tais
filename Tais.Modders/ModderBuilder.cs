using System.Reflection;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using Tais.Commands;
using Tais.Effects;
using Tais.Modders.CommandBuilders;
using Tais.Modders.Conditions;
using Tais.Modders.DataWappers;
using Tais.Modders.DataWappers.Visitors;
using Tais.Modders.Interfaces;
using Tais.Modders.StringBuilders;

namespace Tais.Modders;

public class ModderBuilder
{
    public static IModder Build(string path)
    {
        var nativeMod = Load(Path.Combine(path, "native"));
        return nativeMod;

        //path = @"D:\myproject\Tais\Tais.Mods.Native\bin\Debug\net6.0\Tais.Mods.Native.dll";

        //// The load context needs access to the .Net "core" assemblies...
        //var allAssemblies = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll").ToList();
        //// .. and the assemblies that I need to examine.
        //allAssemblies.Add(path);

        //var resolver = new PathAssemblyResolver(allAssemblies);
        //using (var mlc = new MetadataLoadContext(resolver))
        //{
        //    var assm = mlc.LoadFromAssemblyPath(path);

        //    var type = assm.GetType("Tais.Mods.Native.TaskDefExt");
        //    var obj = Activator.CreateInstance(type, "Test");
        //    throw new Exception();

        //}


    }

    private static Modder Load(string path)
    {
        var alc = AssemblyLoadContext.GetLoadContext(Assembly.GetExecutingAssembly());
        var assembly = alc.LoadFromAssemblyPath(ModPath.GetAssembly(path));

        var types = assembly.ExportedTypes;

        var modder = new Modder()
        {
            PopDefs = types.Where(x => x.BaseType == typeof(PopDef))
                           .Select(x => Activator.CreateInstance(x) as PopDef)
                           .ToDictionary(x => x.PopName, x => x as IPopDef)

        };

        var type = assembly.GetType("Tais.Mods.Native.TaskDefExt");
        var obj = Activator.CreateInstance(type, "Test");

        throw new Exception();
    }

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
                    TaskDefs = new TaskDef[] {
                        new TaskDef()
                        {
                            Name = $"HAO_POP_DEC",
                            RequestActionPoint = 2,
                            Speed = 10,
                            Condition = new PopMinCountCondition(100),
                            CommandBuilders = new[] { new Cmd_ChangePopCount_Builder(new DataWapperConst<float>(0.1f)) },
                        },
                        new TaskDef()
                        {
                            Name = $"HAO_VISIT_FAMILY",
                            RequestActionPoint = 3,
                            Speed = 10,
                            Condition = new TrueCondition(),
                            CommandBuilders = new[] { new Cmd_ChangeFamilyAttitude_Builder(new DataWapperConst<float>(5.0f)) },
                        }
                    }
                },
                new PopDef()
                {
                    PopName = "MIN",
                    IsRegisted = true,
                    HasLiving = true,
                    TaskDefs = new TaskDef[] {
                        new TaskDef()
                        {
                            Name = $"MIN_POP_DEC",
                            RequestActionPoint = 1,
                            Speed = 10,
                            Condition = new PopMinCountCondition(1200),
                            CommandBuilders = new[] { new Cmd_ChangePopCount_Builder(new DataWapperConst<float>(0.1f)) },
                        },
                        new TaskDef()
                        {
                            Name = $"COLLECT_MORE_POP_TAX",
                            RequestActionPoint = 5,
                            Speed = 5,
                            Condition = new TrueCondition(),
                            CommandBuilders = new ICommandBuilder[]
                            {
                                new Cmd_ChangeFinance_Builder(new CityMonthPopTax()),
                                new Cmd_ChangePopLiving_Builder(new DataWapperConst<float>(-10f))
                            },
                        }
                    }
                },
                new PopDef()
                {
                    PopName = "YIN",
                    IsRegisted = false,
                    HasFamily = false,
                    TaskDefs = new TaskDef[] {
                        new TaskDef()
                        {
                            Name = $"YIN_POP_DEC",
                            RequestActionPoint = 3,
                            Speed = 10,
                            Condition = new PopMinCountCondition(12000),
                            CommandBuilders = new[] { new Cmd_ChangePopCount_Builder(new DataWapperConst<float>(0.1f)) },
                        }
                    }
                }
            }.ToDictionary(k => k.PopName, v => v as IPopDef),

            CityDef = new CityDef()
            {
                TaskDefs = new TaskDef[]
                {
                    new TaskDef()
                    {
                        Name = $"SET_CITY0_NO_CONTROLLED",
                        RequestActionPoint = 4,
                        Speed = 5,
                        Condition = new CityNameCondition($"CITY_0"),
                        CommandBuilders = new[] { new Cmd_ChangeCityIsControlFlag_Builder(false) },
                    },
                    new TaskDef()
                    {
                        Name = $"SET_CITY1_NO_CONTROLLED",
                        RequestActionPoint = 5,
                        Speed = 5,
                        Condition = new CityNameCondition($"CITY_1"),
                        CommandBuilders = new[] { new Cmd_ChangeCityIsControlFlag_Builder(false) },
                    },
                },
                BufferDefs = new BufferDef[]
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
                EventDefs = new EventDef[]
                {
                    new EventDef()
                    {
                        Title = new StringBuilder("CentralGov RequestTax Not FullFill, {0} {1}", new CentralGovRequestTax(), new ExpectFinanceYearReserve()),
                        Desc = "CentralGov RequestTax Not FullFill Desc",
                        VaildDate = new VaildDate() { Day = 1 },
                        TriggerCondition = new CentralGovRequestTaxNotFullFill(),
                        Opition = new OpitionDef(){ Desc = "Confirm", CommandBuilder = new Cmd_RevokePlayerTitle_Builder() }
                    },
                    new EventDef()
                    {
                        Title = new StringBuilder("TrueCondition Test"),
                        Desc = "TrueCondition Test Desc",
                        VaildDate = new VaildDate() { Day = 1, Month = 1},
                        TriggerCondition = new TrueCondition(),
                        Opition = new OpitionDef(){ Desc = "Confirm" }
                    },
                },
                WarnDefs = new WarnDef[]
                {
                    new WarnDef()
                    {
                        Name = nameof(CentralGovRequestTaxNotFullFill),
                        Desc = new StringBuilder("CentralGov RequestTax Not FullFill, {0} {1}", new CentralGovRequestTax(), new ExpectFinanceYearReserve()),
                        Condition = new CentralGovRequestTaxNotFullFill()
                    }
                }
            },

            PlayerDef = new PlayerDef()
        };

        return modder;
    }
}
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
    public IEnumerable<TaskDef> TaskDefs { get; init; } = new List<TaskDef>();
    public IEnumerable<BufferDef> BufferDefs { get; init; } = new List<BufferDef>();
    public IEnumerable<EventDef> EventDefs { get; init; } = new List<EventDef>();
    public IEnumerable<WarnDef> WarnDefs { get; init; } = new List<WarnDef>();

    IEnumerable<IBufferDef> IEntityDef.BufferDefs => BufferDefs;

    IEnumerable<ITaskDef> IEntityDef.TaskDefs => TaskDefs;

    IEnumerable<IEventDef> IEntityDef.EventDefs => EventDefs;

    IEnumerable<IWarnDef> IEntityDef.WarnDefs => WarnDefs;
}
