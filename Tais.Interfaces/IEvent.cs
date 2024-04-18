namespace Tais.Interfaces;

public interface IEvent
{
    string Title { get; }
    string Desc { get; }
    IOpition Opition { get; }
}

public interface IOpition
{
    string Desc { get; }
    void OnSelect();
}