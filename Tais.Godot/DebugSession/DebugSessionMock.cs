using System;
using Tais.Interfaces;

public partial class DebugSessionMock : MockControl<DebugSession, ISession>
{
    public override ISession Mock => throw new NotImplementedException();
}

public interface IFoo
{
    string Foobar(String s);
}

public class Bar
{
    public void DoSomethingWithFoo(IFoo foo)
    {
        Console.WriteLine(foo.Foobar("Hello World"));
    }
}