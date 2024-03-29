using System.Collections.Generic;
using System.Linq;
using Tais.InitialDatas.Interfaces;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Pops;

internal class Pop : IPop
{
    public string Name => def.PopName;
    public float Count { get; set; }
    public bool IsRegisted => def.IsRegisted;

    public IFamily Family => family;

    public IEnumerable<ITaskDef> TaskDefs => def.TaskDefs;

    private Family? family;

    private IPopDef def;

    public Pop(IPopDef def, IPopInitData popInitData)
    {
        this.def = def;
        this.Count = popInitData.Count;

        if (def.HasFamily)
        {
            family = new Family(popInitData.FamilyName, new[] { ("Test", 10f) });
        }
    }
}

class Family : IFamily
{
    public string Name { get; }

    public IGroupValue Attitude => attitude;

    private Attitude attitude = new Attitude();

    public Family(string name, IEnumerable<(string desc, float count)> attitudeItems)
    {
        this.Name = name;
        this.attitude.items.AddRange(attitudeItems.Select(x => new Attitude.Item(x.desc, x.count)));
    }
}

class Attitude : IGroupValue
{
    internal class Item
    {
        public string Desc { get; }
        public float Value { get; }

        public Item(string desc, float value)
        {
            Desc = desc;
            Value = value;
        }
    }

    public IEnumerable<(string desc, float count)> Items => items.Select(x => (x.Desc, x.Value));

    public float Current => items.Sum(x => x.Value);

    internal readonly List<Item> items = new List<Item>();
}