namespace Tais.Modders.Interfaces;

public interface ICityDef
{
    IEnumerable<ITaskDef> TaskDefs { get; }
    IEnumerable<IBufferDef> BufferDefs { get; }
}
