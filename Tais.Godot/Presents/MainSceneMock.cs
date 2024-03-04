using Tais.Interfaces;

public partial class MainSceneMock : MockControl<MainScene, ISession>
{
    public override ISession Mock
    {
        get
        {
            return new SessionProxyMock();
        }
    }
}
