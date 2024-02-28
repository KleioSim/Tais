using System;

public partial class DebugSessionMock : MockControl<DebugSession, ISessionProxy>
{
    public override ISessionProxy Mock => throw new NotImplementedException();
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