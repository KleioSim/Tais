using Tais.Interfaces;

public partial class MainSceneMock : MockControl<MainScene>
{
    public override ISession Mock
    {
        get
        {
            return new SessionProxyMock();
        }
    }
}
