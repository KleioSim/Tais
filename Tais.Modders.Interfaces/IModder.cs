using Tais.Commands;

namespace Tais.Modders.Interfaces;

public interface IModder
{
    Dictionary<string, IPopDef> PopDefs { get; }

    ICityDef CityDef { get; }

    Action<ICommand> SendCommand { get; set; }
}
