using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Buffers;

public class Buffer : IBuffer
{
    public IBufferDef def { get; }

    public string Name => def.BufferName;

    public IEnumerable<IEffect> Effects => def.Effects;

    public Buffer(IBufferDef def)
    {
        this.def = def;
    }
}