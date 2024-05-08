using System.Reflection;
using Tais.Modders.DataWappers;
using Tais.Modders.Interfaces;

namespace Tais.Modders.StringBuilders;

public class StringBuilder : IStringBuilder
{
    private readonly string format;
    private readonly IDataWapper[] wappers;

    private static Dictionary<Type, MethodInfo> type2GetValue = new Dictionary<Type, MethodInfo>();

    public StringBuilder(string format, params IDataWapper[] wappers)
    {
        this.format = format;
        this.wappers = wappers;

        foreach (var wapper in wappers)
        {
            var type = wapper.GetType();
            if (type2GetValue.ContainsKey(type))
            {
                continue;
            }

            var getter = type.GetMethod("GetValue");
            if (getter == null)
            {
                throw new Exception();
            }

            type2GetValue.Add(type, getter);
        }
    }

    public string Build(object target)
    {
        var datas = wappers.Select(x => type2GetValue[x.GetType()].Invoke(x, new object[] { target }))
            .ToArray();

        return string.Format(format, datas);
    }
}
