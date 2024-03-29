namespace Tais.InitialDatas.Interfaces;

public interface IInitialData
{
    IEnumerable<ICityInitData> CityInitDatas { get; init; }

    Dictionary<string, IEnumerable<IPopInitData>> City2PopInitDatas { get; init; }
    IPlayerInitData PlayerInitData { get; init; }
}

public interface ICityInitData
{
    string CityName { get; }
    bool IsControlled { get; }
}

public interface IPopInitData
{
    string PopName { get; }
    int Count { get; }
    string FamilyName { get; }
}
public interface IPlayerInitData
{

}