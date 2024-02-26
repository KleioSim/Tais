public partial class MainSceneMock : MockControl<MainScene, ISessionProxy>
{
    public override ISessionProxy Mock
    {
        get
        {
            return new SessionProxyMock();
        }
    }
}
