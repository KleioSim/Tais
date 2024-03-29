﻿using System;
using System.Collections.Generic;
using Tais.Commands;
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

                City2PopInitDatas = new Dictionary<string, IEnumerable<IPopInitData>>()
                {
                    {
                        "CITY_0",
                        new PopInitData[]
                        {
                            new PopInitData()
                            {
                                PopName = "HAO",
                                Count = 100,
                                FamilyName = "FM0"
                            },
                            new PopInitData()
                            {
                                PopName = "MIN",
                                Count = 1000,
                            },
                            new PopInitData()
                            {
                                PopName = "YIN",
                                Count = 10000,
                            }
                        }
                    },
                    {
                        "CITY_1",
                        new PopInitData[]
                        {
                            new PopInitData()
                            {
                                PopName = "HAO",
                                Count = 200,
                                FamilyName = "FM0"
                            },
                            new PopInitData()
                            {
                                PopName = "MIN",
                                Count = 1200,
                            },
                            new PopInitData()
                            {
                                PopName = "YIN",
                                Count = 12000,
                            }
                        }
                    },
                    {
                        "CITY_2",
                        new PopInitData[]
                        {
                            new PopInitData()
                            {
                                PopName = "HAO",
                                Count = 300,
                                FamilyName = "FM0"
                            },
                            new PopInitData()
                            {
                                PopName = "MIN",
                                Count = 1300,
                            },
                            new PopInitData()
                            {
                                PopName = "YIN",
                                Count = 13000,
                            }
                        }
                    },
                }
            };

            var modder = ModderBuilder.Build();
            var session = SessionBuilder.Build(initData, modder);

            CommandSender.Send = session.OnCommand;

            return session;
        }
    }
}