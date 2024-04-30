using Tais.Modders;

namespace Tais.Mods.Native;

public class NativeTest
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

public class NativeTest2
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

public class NativeTest4
{
    public int Add(int a, int b)
    {
        return a + b;
    }
}

public class TaskDefExt : TaskDef
{
    public TaskDefExt(string name)
    {
        Name = name;
    }

    public string GetName()
    {
        return Name;
    }
}

public class TaskDefExt2 : TaskDef
{
    public TaskDefExt2(string name)
    {
        Name = name;
    }

    public string GetName()
    {
        return Name;
    }
}