namespace Tais.Modders.DataWappers;

interface IDataWapper<T> : IDataWapper
{
    T GetValue(object target);
}

interface IDataWapper
{

}