using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Tasks;

class Task : ITask
{
    public ITaskDef Def { get; }
    public IEntity Target { get; }

    public float Progress { get; set; }

    public Task(ITaskDef def, IEntity target)
    {
        this.Def = def;
        this.Target = target;
    }
}
