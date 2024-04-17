namespace Tais.Modders.Interfaces;

public interface IEntityDef
{
    IEnumerable<IBufferDef> BufferDefs { get; }
    IEnumerable<ITaskDef> TaskDefs { get; }
    IEnumerable<IEventDef> EventDefs { get; }
    IEnumerable<IWarnDef> WarnDefs { get; }
}