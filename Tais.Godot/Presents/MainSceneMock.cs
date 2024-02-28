public partial class MainSceneMock : MockControl<MainScene, ISessionProxy>
{
    public override ISessionProxy Mock
    {
        get
        {
            var session = new SessionProxyMock();
            session.finance.Current = 100;
            session.finance.Surplus = 10;

            return session;
        }
    }
}
