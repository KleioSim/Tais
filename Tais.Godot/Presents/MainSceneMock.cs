using Tais.Interfaces;

public partial class MainSceneMock : MockControl<MainScene, ISession>
{
    public override ISession Mock
    {
        get
        {
            var session = new SessionMock();
            session.finance.Current = 100;
            session.finance.Surplus = 10;

            return session;
        }
    }
}
