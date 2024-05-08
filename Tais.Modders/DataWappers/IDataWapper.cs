namespace Tais.Modders.DataWappers;

public interface IDataWapper<T> : IDataWapper
{
    T GetValue(object target);
}

public interface IDataWapper
{

}