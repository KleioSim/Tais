using Tais.Interfaces;

public abstract partial class MockControl<TView> : MockControl<TView, ISession>
    where TView : ViewControl
{

}