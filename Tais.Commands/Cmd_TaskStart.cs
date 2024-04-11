namespace Tais.Commands;

public class Cmd_TaskStart : AbsCommand
{
    public object Def { get; }

    public override string Desc { get; }

    public Cmd_TaskStart(object def, object target)
    {
        this.Def = def;
        this.Target = target;
    }
}

public class Cmd_ChangeFamilyAttitude : AbsCommand
{
    public override string Desc => $"{nameof(Cmd_ChangeFamilyAttitude)} {Value.ToString("+0;-#")}";

    public float Value { get; }

    public Cmd_ChangeFamilyAttitude(float value)
    {
        Value = value;
    }
}

[ExportCheatCommand]
public class Cmd_ChangePopCount : AbsCommand
{
    public override string Desc => $"{nameof(Cmd_ChangePopCount)} {Value.ToString("+0%;-#%")}";

    public float Value { get; }

    [CheatConstructors]
    public Cmd_ChangePopCount(float value)
    {
        Value = value;
    }
}

[ExportCheatCommand]
public class Cmd_ChangeCityIsControlFlag : AbsCommand
{
    public override string Desc => $"{nameof(Cmd_ChangeCityIsControlFlag)} {Value}";

    public bool Value { get; }

    [CheatConstructors]
    public Cmd_ChangeCityIsControlFlag(bool value)
    {
        Value = value;
    }
}

[ExportCheatCommand]
public class Cmd_RevokePlayerTitle : ICommand
{
    public string Desc => $"{nameof(Cmd_RevokePlayerTitle)}";

    public string Reason { get; set; }

    [CheatConstructors]
    public Cmd_RevokePlayerTitle()
    {
    }
}

[ExportCheatCommand]
public class Cmd_ChangeCentralGovTaxLevel : ICommand
{
    public string Desc => $"{nameof(Cmd_RevokePlayerTitle)}";

    public string Reason { get; set; }

    public string Value { get; }

    [CheatConstructors]
    public Cmd_ChangeCentralGovTaxLevel(string value)
    {
        Value = value;
    }
}

public class ExportCheatCommand : Attribute
{

}

public class CheatConstructors : Attribute
{

}