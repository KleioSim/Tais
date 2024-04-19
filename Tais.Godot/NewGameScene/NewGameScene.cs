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

            var global = GetNode<Global>("/root/Global");

            global.session = SessionBuilder.Build(initData, global.modder);

            OnEnterGame.Invoke();
        };

        Back.Pressed += () => OnBack.Invoke();
    }

}
