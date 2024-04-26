using Tais.Commands;
using Tais.Interfaces;
using Tais.Modders.Interfaces;

namespace Tais.Tasks;

class Task : ITask
{
    public ITaskDef Def { get; }
    public object Target { get; }

    public float Progress
    {
        get => progress;
        internal set
        {
            progress = value;
            if (Progress >= 100)
            {
                foreach (var cmdBuilder in Def.CommandBuilders)
                {
                    var cmd = cmdBuilder.Build(Target);

                    cmd.Reason = Def.Name;

                    if (cmd is ICommandWithTarget cmdWithTarget)
                    {
                        cmdWithTarget.Target = Target;
                    }

                    CommandSender.Send(cmd);
                }
            }
        }
    }

    private float progress;

    public Task(ITaskDef def, object target)
    {
        this.Def = def;
        this.Target = target;
    }

    internal void OnDaysInc(IDate date)
    {
        Progress += Def.Speed;
    }
}
