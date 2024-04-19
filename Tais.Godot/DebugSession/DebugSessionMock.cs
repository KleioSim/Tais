using Tais.InitialDatas;
using Tais.InitialDatas.Interfaces;
using Tais.Interfaces;
using Tais.Modders;
using Tais.Sessions;

public partial class DebugSessionMock : MockControl<DebugSession, ISession>
{
    public override ISession Mock
    {
        get
        {
            var initData = new InitialData()
            {
                CentralGovInitData = new CentralGovInitData()
                {
                    TaxLevel = "MID"
                },

                CityInitDatas = new ICityInitData[]
                {
                    new CityInitData()
                    {
                        CityName = "CITY_0",
                        IsControlled = true
                    },
                    new CityInitData()
                    {
                        CityName = "CITY_1",
                        IsControlled = true
                    },
                    new CityInitData()
                    {
                        CityName = "CITY_2",
                        IsControlled = false
                    }
                },

                PopInitDatas = new PopInitData[]
                {
                    new PopInitData()
                    {
                        CityName =  "CITY_0",
                        PopName = "HAO",
                        Count = 100,
                        FamilyName = "FM0"
                    },
                    new PopInitData()
                    {
                        CityName =  "CITY_0",
                        PopName = "MIN",
                        Count = 1000,
                    },
                    new PopInitData()
                    {
                         CityName =  "CITY_0",
                         PopName = "YIN",
                         Count = 10000,
                    },
                    new PopInitData()
                    {
                        CityName =  "CITY_1",
                        PopName = "HAO",
                        Count = 200,
                        FamilyName = "FM0"
                    },
                    new PopInitData()
                    {
                        CityName =  "CITY_1",
                        PopName = "MIN",
                        Count = 1200,
                    },
                    new PopInitData()
                    {
                        CityName =  "CITY_1",
                        PopName = "YIN",
                        Count = 12000,
                    },
                    new PopInitData()
                    {
                        CityName =  "CITY_2",
                        PopName = "HAO",
                        Count = 300,
                        FamilyName = "FM0"
                    },
                    new PopInitData()
                    {
                        CityName =  "CITY_2",
                        PopName = "MIN",
                        Count = 1300,
                    },
                    new PopInitData()
                    {
                        CityName =  "CITY_2",
                        PopName = "YIN",
                        Count = 13000,
                    }
                }
            };

            var global = GetNode<Global>("/root/Global"); global.modder = ModderBuilder.Build();
            global.session = SessionBuilder.Build(initData, global.modder);
            return global.session;
        }
    }
}