using Tais.Citys;
using Tais.InitialDatas.Interfaces;
using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.Pops;

namespace Tais.Sessions;

public static class SessionBuilder
{
    public static ISession Build(IInitialData initialData, IModder modder)
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

