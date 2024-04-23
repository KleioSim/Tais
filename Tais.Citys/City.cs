using System;
using System.Collections.Generic;
using System.Linq;
using Tais.Effects;
using Tais.Entities;
using Tais.InitialDatas.Interfaces;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Citys;

internal class City : Entity<ICityDef>, ICity
{
    public static Func<string, IEnumerable<IPop>>? GetPops;
    public static Action<bool, City>? OnOwnerChanged;

    public string Name { get; }
    public IEffectValue PopTax => popTax;
    public IGroupValue RegistPopCount => popTax.RegisterPopCount;
    public IEnumerable<IPop> Pops => GetPops(Name);
    public IEnumerable<IEffect> Effects => Buffers.SelectMany(x => x.Effects);

    public bool IsOwned
    {
        get => isOwned;
        set
        {
            isOwned = value;
            OnOwnerChanged?.Invoke(isOwned, this);
        }
    }

    public IEnumerable<ITaskDef> TaskDefs => Def.TaskDefs;


    private PopTax popTax;

    private bool isOwned;

    public City(string id, ICityDef def, ICityInitData initData) : base(id, def)
    {
        popTax = new PopTax(this);

        Name = initData.CityName;
        IsOwned = initData.IsControlled;
    }

    internal void OnDaysInc(IDate date)
    {
    }
}

class PopTax : IEffectValue
{
    public IGroupValue RegisterPopCount => new GroupValue(from.Pops.Where(x => x.IsRegisted).Select(x => (x.Name, x.Count)));

    public float BaseValue => RegisterPopCount.Current / 10000f;

    public IEnumerable<IEffect> Effects => from.Effects.OfType<PopTaxChangePercentEffect>();

    private City from;

    public PopTax(City city)
    {
        this.from = city;
    }
}

class GroupValue : IGroupValue
{
    public float Current => Items.Sum(x => x.count);

    public virtual IEnumerable<(string desc, float count)> Items { get; }

    public GroupValue(IEnumerable<(string desc, float count)> items)
    {
        Items = items;
    }
}