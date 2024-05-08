using Tais.Modders.Interfaces;

namespace Tais.Modders;

public class BufferDef : IBufferDef
{
    public string BufferName { get; init; }

    public ICondition ValidCondition { get; init; }

    public ICondition InvalidCondition { get; init; }

    public IEnumerable<IEffect> Effects { get; init; }
}

public class BufferDef<T> : BufferDef
    where T : EntityDef
{

}