namespace Tais.Modders.Interfaces;

public interface IBufferDef
{
    string BufferName { get; }

    ICondition ValidCondition { get; }
    ICondition InvalidCondition { get; }

    IEnumerable<IEffect> Effects { get; }
}
