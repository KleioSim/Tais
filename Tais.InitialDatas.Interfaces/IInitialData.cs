namespace Tais.InitialDatas.Interfaces;

public interface IInitialData
{
    IEnumerable<ICityInitData> CityInitDatas { get; init; }

    IEnumerable<IPopInitData> PopInitDatas { get; init; }
    IPlayerInitData PlayerInitData { get; init; }
    ICentralGovInitData CentralGovInitData { get; }
}

public interface IEntityInitial
{

}

public interface ICityInitData : IEntityInitial
{
    string CityName { get; }
    bool IsControlled { get; }
}

public interface IPopInitData : IEntityInitial
{
    string CityName { get; }
    string PopName { get; }
    int Count { get; }
    string FamilyName { get; }
}
public interface IPlayerInitData : IEntityInitial
{

}

public interface ICentralGovInitData : IEntityInitial
{
    string TaxLevel { get; }
}