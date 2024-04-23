namespace Tais.Commands;

[ExportCheatCommand]
public class Cmd_ChangeFinance : ICommand
{
    public string Desc => $"{nameof(Cmd_ChangeFinance)}";

    public string Reason { get; set; }

    public float Value { get; }

    [CheatConstructors]
    public Cmd_ChangeFinance(float value)
    {
        Value = value;
    }
}
