using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

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

public static class BufferProcess
{
    public static ISession Session { get; set; }

    public static Action<IBuffer, object>? OnBufferAdded;
    public static Action<IBuffer, object>? OnBufferRemoved;

    public static void Do<T>(IEnumerable<IBufferDef> defs, T target, List<Buffer> buffers)
        where T : IEntity
    {
        var context = new ProcessContext { current = target, session = Session };

        for (int i = buffers.Count - 1; i >= 0; i--)
        {
            var currBuff = buffers[i];
            if (currBuff.def.InvalidCondition.IsSatisfied(context))
            {

                buffers.Remove(currBuff);

                OnBufferRemoved?.Invoke(currBuff, target);
            }
        }

        foreach (var def in defs)
        {
            if (buffers.Any(x => x.def == def))
            {
                continue;
            }

            if (!def.ValidCondition.IsSatisfied(context))
            {
                continue;
            }

            var newBuff = new Buffers.Buffer(def);
            buffers.Add(newBuff);

            OnBufferAdded?.Invoke(newBuff, target);
        }
    }
}