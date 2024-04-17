using Tais.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Tais.Effects;
using Tais.Modders.Interfaces;
using Tais.InitialDatas.Interfaces;
using Tais.Entities;
using Tais.Buffers;

namespace Tais.Citys;

internal class City : Entity<ICityDef>, ICity
{
    public static Action<bool, City>? OnOwnerChanged;
    public static Action<IBuffer, ICity>? OnBufferAdded;
    public static Action<IBuffer, ICity>? OnBufferRemoved;

    public string Name { get; }
    public IEffectValue PopTax => popTax;
    public IGroupValue PopCount => popCount;
    public IEnumerable<IPop> Pops => pops;
    public IEnumerable<IBuffer> Buffers => buffers;
    public IEnumerable<IEffect> Effects => buffers.SelectMany(x => x.Effects);

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
    private GroupValue popCount;

    private bool isOwned;
    private List<IPop> pops = new List<IPop>();
    private List<Buffers.Buffer> buffers = new List<Buffers.Buffer>();

    public City(ICityDef def, ICityInitData initData, IEnumerable<IPop> pops)
    {
        this.Def = def;

        popTax = new PopTax(this);
        popCount = new GroupValue();
        popCount.Items = pops.Where(x => x.IsRegisted).Select(x => (x.Name, x.Count));

        Name = initData.CityName;
        IsOwned = initData.IsControlled;

        this.pops.AddRange(pops);
    }

    internal void OnDaysInc(IDate date)
    {
        BufferProcess.Do(Def.BufferDefs, this, buffers);

        //for (int i = buffers.Count - 1; i >= 0; i--)
        //{
        //    var currBuff = buffers[i];
        //    if (currBuff.def.InvalidCondition.IsSatisfied(this))
        //    {

        //        buffers.Remove(currBuff);

        //        OnBufferRemoved?.Invoke(currBuff, this);
        //    }
        //}

        //foreach (var def in def.BufferDefs)
        //{
        //    if (buffers.Any(x => x.def == def))
        //    {
        //        continue;
        //    }

        //    if (!def.ValidCondition.IsSatisfied(this))
        //    {
        //        continue;
        //    }

        //    var newBuff = new Buffers.Buffer(def);
        //    buffers.Add(newBuff);

        //    OnBufferAdded?.Invoke(newBuff, this);
        //}
    }
}

class PopTax : IEffectValue
{
    public float BaseValue => from.PopCount.Current / 10000f;

    public IEnumerable<IEffect> Effects => from.Effects.OfType<PopTaxChangePercentEffect>();

    private ICity from;

    public PopTax(ICity city)
    {
        this.from = city;
    }
}


class GroupValue : IGroupValue
{
    public float Current => Items.Sum(x => x.count);

    public virtual IEnumerable<(string desc, float count)> Items { get; set; }
}