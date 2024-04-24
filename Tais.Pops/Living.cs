using System.Collections.Generic;
using System.Linq;
using Tais.Interfaces;

namespace Tais.Pops;

class Living : IGroupValue
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

    public Living(float baseValue)
    {
        items.Add(new Item("base value", baseValue));
    }
}