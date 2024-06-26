﻿using Godot;
using System.Reflection;
using System;
using Tais.Interfaces;
using Tais.InitialDatas;
using Tais.Modders;
using Tais.Sessions;
using Tais.InitialDatas.Interfaces;
using System.IO;

public partial class DebugSession : Control
{
    public override void _Ready()
    {
        PresentBase.IsMock = false;

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
        global.modder = ModderBuilder.Build(Global.ModPath);

        global.session = SessionBuilder.Build(initData, global.modder);
    }

    public override void _Process(double delta)
    {
        GetTree().ChangeSceneToFile("res://MainScene/MainScene.tscn");
    }
}