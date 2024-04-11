using Godot;
using System;
using System.Collections.Generic;
using Tais.InitialDatas;
using Tais.InitialDatas.Interfaces;
using Tais.Modders;
using Tais.Sessions;

public partial class NewGameScene : Control
{
    public static Action OnEnterGame { get; set; }
    public static Action OnBack { get; set; }

    public Button EnterGame => GetNode<Button>("VBoxContainer/Enter");
    public Button Back => GetNode<Button>("VBoxContainer/Back");

    static NewGameScene()
    {
        OnEnterGame += () => GD.Print("NewGameScene OnEnterGame");
        OnBack += () => GD.Print("NewGameScene OnBack");
    }

    public override void _Ready()
    {
        base._Ready();

        EnterGame.Pressed += () =>
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

            var global = GetNode<Global>("/root/Global");

            global.session = SessionBuilder.Build(initData, global.modder);

            OnEnterGame.Invoke();
        };

        Back.Pressed += () => OnBack.Invoke();
    }

}
