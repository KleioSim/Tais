using Tais.Citys;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.Pops;

namespace Tais.Sessions;

public static class SessionBuilder
{
    public static ISession Build(InitialData initialData, IModder modder)
    {
        var session = new Session();

        session.player.Initialize(initialData.PlayerInitData);

        foreach (var cityInitData in initialData.CityInitDatas)
        {
            var pops = initialData.City2PopInitDatas[cityInitData.CityName].Select(popInit => new Pop(modder.PopDefs[popInit.PopName], popInit));
            var city = new City(modder.CityDef, cityInitData, pops);

            session.cities.Add(city);
        }

        session.centralGov.InitTaxValue = session.finance.incomes.Sum(x => x.CurrValue) * 0.8f;

        return session;
    }
}

public class InitialData
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