using System;
using Tais.Interfaces;
using Tais.Sessions;

public partial class DebugSessionMock : MockControl<DebugSession, ISession>
{
    public override ISession Mock
    {
        get
        {
            return SessionBuilder.Build();
        }
    }
}