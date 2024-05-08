using System.Xml.Linq;
using Tais.Modders;
using Tais.Modders.CommandBuilders;
using Tais.Modders.Conditions;
using Tais.Modders.DataWappers;
using Tais.Modders.DataWappers.Visitors;
using Tais.Modders.Interfaces;
using Tais.Modders.StringBuilders;

namespace Tais.Mods.Native;

public class PopHao : PopDef
{
    public PopHao()
    {
        PopName = "HAO";
        IsRegisted = true;
        HasFamily = true;
    }
}

public class PopMin : PopDef
{
    public PopMin()
    {
        PopName = "MIN";
        IsRegisted = true;
        HasLiving = true;
    }
}

public class PopYin : PopDef
{
    public PopYin()
    {
        PopName = "YIN";
    }
}

public class Task_HAO_POP_DEC : TaskDef<PopHao>
{
    public Task_HAO_POP_DEC()
    {
        Name = $"HAO_POP_DEC";
        RequestActionPoint = 2;
        Speed = 10;
        Condition = new PopMinCountCondition(100);
        CommandBuilders = new[] { new Cmd_ChangePopCount_Builder(new DataWapperConst<float>(0.1f)) };
    }
}

public class Task_HAO_VISIT_FAMILY : TaskDef<PopHao>
{
    public Task_HAO_VISIT_FAMILY()
    {
        Name = $"HAO_VISIT_FAMILY";
        RequestActionPoint = 3;
        Speed = 10;
        Condition = new TrueCondition();
        CommandBuilders = new[] { new Cmd_ChangeFamilyAttitude_Builder(new DataWapperConst<float>(5.0f)) };
    }
}

public class Task_MIN_POP_DEC : TaskDef<PopMin>
{
    public Task_MIN_POP_DEC()
    {
        Name = $"HAO_POP_DEC";
        RequestActionPoint = 2;
        Speed = 10;
        Condition = new PopMinCountCondition(1200);
        CommandBuilders = new[] { new Cmd_ChangePopCount_Builder(new DataWapperConst<float>(0.1f)) };
    }
}

public class Task_MIN_COLLECT_MORE_POP_TAX : TaskDef<PopMin>
{
    public Task_MIN_COLLECT_MORE_POP_TAX()
    {
        RequestActionPoint = 5;
        Speed = 5;
        Condition = new TrueCondition();
        CommandBuilders = new ICommandBuilder[]
        {
            new Cmd_ChangeFinance_Builder(new CityMonthPopTax()),
            new Cmd_ChangePopLiving_Builder(new DataWapperConst<float>(-10f))
        };
    }
}

public class Task_YIN_POP_DEC : TaskDef<PopYin>
{
    public Task_YIN_POP_DEC()
    {
        Name = $"YIN_POP_DEC";
        RequestActionPoint = 3;
        Speed = 10;
        Condition = new PopMinCountCondition(12000);
        CommandBuilders = new[] { new Cmd_ChangePopCount_Builder(new DataWapperConst<float>(0.1f)) };
    }
}

public class Task_SET_CITY0_NO_CONTROLLED : TaskDef<CityDef>
{
    public Task_SET_CITY0_NO_CONTROLLED()
    {
        Name = $"SET_CITY0_NO_CONTROLLED";
        RequestActionPoint = 4;
        Speed = 5;
        Condition = new CityNameCondition($"CITY_0");
        CommandBuilders = new[] { new Cmd_ChangeCityIsControlFlag_Builder(false) };
    }
}

public class Task_SET_CITY1_NO_CONTROLLED : TaskDef<CityDef>
{
    public Task_SET_CITY1_NO_CONTROLLED()
    {
        Name = $"SET_CITY0_NO_CONTROLLED";
        RequestActionPoint = 4;
        Speed = 5;
        Condition = new CityNameCondition($"CITY_1");
        CommandBuilders = new[] { new Cmd_ChangeCityIsControlFlag_Builder(false) };
    }
}

public class Buffer_CITY_HAO_ATTITUDE_HELP : BufferDef<CityDef>
{
    public Buffer_CITY_HAO_ATTITUDE_HELP()
    {
        BufferName = "HAO_ATTITUDE_HELP";
        ValidCondition = new FamilyAttitudeMin(20);
        InvalidCondition = new FamilyAttitudeMax(10);
        Effects = new IEffect[]
        {
            new PopTaxChangePercentEffect("HAO_ATTITUDE_HELP", 0.15f)
        };
    }
}

public class Warn_CentralGovRequestTaxNotFullFill : WarnDef<CentralGovDef>
{
    public Warn_CentralGovRequestTaxNotFullFill()
    {
        Name = nameof(CentralGovRequestTaxNotFullFill);
        Desc = new StringBuilder("CentralGov RequestTax Not FullFill, {0} {1}", new CentralGovRequestTax(), new ExpectFinanceYearReserve());
        Condition = new CentralGovRequestTaxNotFullFill();
    }
}


public class Event_CentralGovRequestTaxNotFullFill : EventDef<CentralGovDef>
{
    public Event_CentralGovRequestTaxNotFullFill()
    {
        Title = new StringBuilder("CentralGov RequestTax Not FullFill, {0} {1}", new CentralGovRequestTax(), new ExpectFinanceYearReserve());

        Desc = "CentralGov RequestTax Not FullFill Desc";
        VaildDate = new VaildDate() { Day = 1 };
        TriggerCondition = new CentralGovRequestTaxNotFullFill();
        Opition = new OpitionDef() { Desc = "Confirm", CommandBuilder = new Cmd_RevokePlayerTitle_Builder() };
    }
}

public class Event_TrueConditionTest : EventDef<CentralGovDef>
{
    public Event_TrueConditionTest()
    {
        Title = new StringBuilder("TrueCondition Test");
        Desc = "TrueCondition Test Desc";
        VaildDate = new VaildDate() { Day = 1, Month = 1 };
        TriggerCondition = new TrueCondition();
        Opition = new OpitionDef() { Desc = "Confirm" };
    }
}