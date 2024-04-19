using Tais.Entities;
using Tais.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Buffers;

public class BufferManager
{
    private readonly ISession session;

    public BufferManager(ISession session)
    {
        this.session = session;
    }

    public void OnDaysInc()
    {
        foreach (Entity entity in session.Entities)
        {
            var context = new ProcessContext { current = entity, session = session };

            var buffers = entity.Buffers.OfType<Buffer>().ToArray();
            for (int i = buffers.Length - 1; i >= 0; i--)
            {
                var currBuff = buffers[i] as Buffer;
                if (currBuff.def.InvalidCondition.IsSatisfied(context))
                {

                    entity.RemoveBuffer(currBuff);
                }
            }

            foreach (var def in entity.Def.BufferDefs)
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
                entity.AddBuffer(newBuff);
            }
        }
    }
}