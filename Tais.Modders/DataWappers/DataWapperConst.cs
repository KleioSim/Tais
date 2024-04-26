namespace Tais.Modders.DataWappers;

class DataWapperConst<T> : IDataWapper<T>
{
    private T data;

    public DataWapperConst(T data)
    {
        this.data = data;
    }

    public T GetValue(object target)
    {
        return data;
    }
}
