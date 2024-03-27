using Tais.Commands;
using Tais.Modders.Interfaces;

namespace Tais.Modders;

class Modder : IModder
{
    public Dictionary<string, IPopDef> PopDefs { get; init; }

    public ICityDef CityDef { get; init; }

    public Action<ICommand> SendCommand
    {
        get => CommandSender.Send;
        set
        {
            CommandSender.Send = value;
        }
    }
}

static class CommandSender
{
    public static Action<ICommand> Send { get; set; }
}