namespace Tais.InitialDatas.Interfaces;

public interface IInitialData
{
    IEnumerable<ICityInitData> CityInitDatas { get; init; }

    IEnumerable<IPopInitData> PopInitDatas { get; init; }
    IPlayerInitData PlayerInitData { get; init; }
    ICentralGovInitData CentralGovInitData { get; }
}

public interface ICityInitData
{
    string CityName { get; }
    bool IsControlled { get; }
}

public interface IPopInitData
{
    string CityName { get; }
    string PopName { get; }
    int Count { get; }
    string FamilyName { get; }
}
public interface IPlayerInitData
{

}

public interface ICentralGovInitData
{
    string TaxLevel { get; }
}