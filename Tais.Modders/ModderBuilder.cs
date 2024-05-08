using System.Reflection;
using System.Runtime.Loader;
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
                           .ToDictionary(x => x.PopName, x => x as IPopDef),
            CityDef = new CityDef(),

            PlayerDef = new PlayerDef(),

            CentralGovDef = new CentralGovDef(),
        };

        foreach (var entityDef in modder.EntityDefs)
        {
            var taskDefType = typeof(TaskDef<>).MakeGenericType(entityDef.GetType());

            entityDef.TaskDefs = types.Where(x => x.BaseType == taskDefType)
                .Select(x => Activator.CreateInstance(x) as TaskDef)
                .ToArray();

            var buffDefType = typeof(BufferDef<>).MakeGenericType(entityDef.GetType());

            entityDef.BufferDefs = types.Where(x => x.BaseType == buffDefType)
                .Select(x => Activator.CreateInstance(x) as BufferDef)
                .ToArray();

            var warnDefType = typeof(WarnDef<>).MakeGenericType(entityDef.GetType());

            entityDef.WarnDefs = types.Where(x => x.BaseType == warnDefType)
                .Select(x => Activator.CreateInstance(x) as WarnDef)
                .ToArray();

            var eventDefType = typeof(EventDef<>).MakeGenericType(entityDef.GetType());

            entityDef.EventDefs = types.Where(x => x.BaseType == eventDefType)
                .Select(x => Activator.CreateInstance(x) as EventDef)
                .ToArray();
        }

        return modder;
    }
}

public class PlayerDef : EntityDef, IPlayerDef
{

}


public class CityDef : EntityDef, ICityDef
{

}

public class EntityDef : IEntityDef
{
    public IEnumerable<IBufferDef> BufferDefs { get; set; } = Enumerable.Empty<IBufferDef>();

    public IEnumerable<ITaskDef> TaskDefs { get; set; } = Enumerable.Empty<ITaskDef>();

    public IEnumerable<IEventDef> EventDefs { get; set; } = Enumerable.Empty<IEventDef>();

    public IEnumerable<IWarnDef> WarnDefs { get; set; } = Enumerable.Empty<IWarnDef>();
}
