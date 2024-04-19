using Tais.CentralGovs;
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

        session.entityManager.Add(new Player(initialData.PlayerInitData, modder.PlayerDef));
        session.entityManager.Add(new CentralGov(initialData.CentralGovInitData, modder.CentralGovDef));

        foreach (var cityInitData in initialData.CityInitDatas)
        {
            var city = new City(modder.CityDef, cityInitData);
            session.entityManager.Add(city);
        }

        foreach (var popInitData in initialData.PopInitDatas)
        {
            var pop = new Pop(modder.PopDefs[popInitData.PopName], popInitData);
            session.entityManager.Add(pop);
        }

        //session.centralGov.InitTaxValue = session.finance.incomes.Sum(x => x.CurrValue) * 0.8f * 12;

        return session;
    }
}

