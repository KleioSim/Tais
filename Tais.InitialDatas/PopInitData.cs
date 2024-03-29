using Tais.InitialDatas.Interfaces;

namespace Tais.InitialDatas;

public class PopInitData : IPopInitData
{
    public int Count { get; init; }

    public string FamilyName { get; init; }

    public string PopName { get; init; }
}

public class InitialData : IInitialData
{
    public IEnumerable<ICityInitData> CityInitDatas { get; init; }

    public Dictionary<string, IEnumerable<IPopInitData>> City2PopInitDatas { get; init; }
    public IPlayerInitData PlayerInitData { get; init; }
}

public class CityInitData : ICityInitData
{
    public string CityName { get; init; }

    public bool IsControlled { get; init; }
}