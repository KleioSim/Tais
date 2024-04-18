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

            session.GenerateCity("CITY_0", 1000, true);
            session.GenerateCity("CITY_1", 2000, false);
            session.GenerateCity("CITY_2", 3000, true);

            return session;
        }
    }
}
