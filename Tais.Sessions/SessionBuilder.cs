using System.Linq;
using Tais.CentralGovs;
using Tais.Citys;
using Tais.Entities;
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

        session.entityManager.CreatePlayer(modder.PlayerDef, initialData.PlayerInitData);

        foreach (var cityInitData in initialData.CityInitDatas)
        {
            session.entityManager.CreateCity(modder.CityDef, cityInitData);
        }

        foreach (var popInitData in initialData.PopInitDatas)
        {
            session.entityManager.CreatePop(modder.PopDefs[popInitData.PopName], popInitData);
        }

        var centralGov = session.entityManager.CreateCentralGov(modder.CentralGovDef, initialData.CentralGovInitData);
        centralGov.ReportPopCount = session.Cities.Sum(x => (int)x.RegistPopCount.Current);

        return session;
    }
}

static class EntityExtension
{
    public static Player CreatePlayer(this EntityManager entityManager, IPlayerDef def, IPlayerInitData initData)
    {
        var player = new Player(entityManager.GenerateId(), def, initData);
        entityManager.AddEntity(player);
        return player;
    }

    public static CentralGov CreateCentralGov(this EntityManager entityManager, ICentralGovDef def, ICentralGovInitData initData)
    {
        var centralGov = new CentralGov(entityManager.GenerateId(), def, initData);
        entityManager.AddEntity(centralGov);
        return centralGov;
    }

    public static City CreateCity(this EntityManager entityManager, ICityDef def, ICityInitData initData)
    {
        var city = new City(entityManager.GenerateId(), def, initData);
        entityManager.AddEntity(city);
        return city;
    }

    public static Pop CreatePop(this EntityManager entityManager, IPopDef def, IPopInitData initData)
    {
        var pop = new Pop(entityManager.GenerateId(), def, initData);
        entityManager.AddEntity(pop);
        return pop;
    }
}