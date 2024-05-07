using Tais.Modders;
using Tais.Modders.Conditions;

namespace Tais.Mods.Native;

public class PopHao : PopDef
{
    //public override string PopName { get; init; }

    //public override bool IsRegisted { get; init; }

    //public override bool HasFamily { get; init; }

    //public override bool HasLiving { get; init; }

    public PopHao()
    {
        PopName = "HAO";
        IsRegisted = true;
        HasFamily = true;

        //TaskDefs = new TaskDef[] {
        //                new TaskDef()
        //                {
        //                    Name = $"HAO_POP_DEC",
        //                    RequestActionPoint = 2,
        //                    Speed = 10,
        //                    Condition = new PopMinCountCondition(100),
        //                    CommandBuilders = new[] { new Cmd_ChangePopCount_Builder(new DataWapperConst<float>(0.1f)) },
        //                },
        //                new TaskDef()
        //                {
        //                    Name = $"HAO_VISIT_FAMILY",
        //                    RequestActionPoint = 3,
        //                    Speed = 10,
        //                    Condition = new TrueCondition(),
        //                    CommandBuilders = new[] { new Cmd_ChangeFamilyAttitude_Builder(new DataWapperConst<float>(5.0f)) },
        //                }
        //            };
    }
}
