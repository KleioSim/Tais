namespace Tais.Interfaces;

public interface IEvent
{
    string Title { get; }
    string Desc { get; }

    void OnSelect();
}
