namespace Tais.Modders.DataWappers;

interface IDataWapper<T>
{
    T GetValue(object target);
}
