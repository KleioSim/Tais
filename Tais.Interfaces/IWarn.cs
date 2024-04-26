namespace Tais.Interfaces;

public interface IWarn
{
    public string Name { get; }
    public IEnumerable<string> Items { get; }
}