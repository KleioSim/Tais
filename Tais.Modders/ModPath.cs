namespace Tais.Modders;

class ModPath
{

    public static string GetAssembly(string path) => Directory.EnumerateFiles(Path.Combine(path, "assembly"), "*.dll").Single();
}