namespace Tais.Commands;

[ExportCheatCommand]
public class Cmd_ChangePopLiving : AbsCommand
{
    public override string Desc => $"{nameof(Cmd_ChangePopLiving)}";

    public float Value { get; }

    [CheatConstructors]
    public Cmd_ChangePopLiving(float value)
    {
        Value = value;
    }
}
