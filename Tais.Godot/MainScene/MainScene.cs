﻿using Godot;
using System;

public partial class MainScene : ViewControl
{
    [Signal]
    public delegate void NextTurnStartedEventHandler();

    [Signal]
    public delegate void NextTurnFinishedEventHandler();

    internal Button CreateTask => GetNode<Button>("CreateTask");
    internal Button CreateEvent { get; }
    internal Button NextTurn => GetNode<Button>("NextTurn");

    internal Button City => GetNode<Button>("TopInfo/City");
    internal Label CityCount => GetNode<Label>("TopInfo/City/HBoxContainer/Count");

    internal Label FinanceCurrent => GetNode<Label>("TopInfo/Finance/HBoxContainer/Current");
    internal Label FinanceSurplus => GetNode<Label>("TopInfo/Finance/HBoxContainer/Surplus");
    internal Label PopCount => GetNode<Label>("TopInfo/Pop/HBoxContainer/Count");

    internal Label Year => GetNode<Label>("TopInfo/Date/HBoxContainer/Year/Value");
    internal Label Month => GetNode<Label>("TopInfo/Date/HBoxContainer/Month/Value");
    internal Label Day => GetNode<Label>("TopInfo/Date/HBoxContainer/Day/Value");

    internal LeftPanel LeftPanel => GetNode<LeftPanel>("LeftPanel");
    internal InstancePlaceholder EventDialogHolder => GetNode<InstancePlaceholder>("EventDialog");

    internal Timer NextDayTimer => GetNode<Timer>("NextDayTimer");

    internal Label FreeActionPoint => GetNode<Label>("TopInfo/Player/ActionPoint/Free");
    internal Label TotalActionPoint => GetNode<Label>("TopInfo/Player/ActionPoint/Total");

    internal ItemContainer<TaskItem> TaskContainer;
    internal ItemContainer<ToastItem> ToastContainer;

    public override void _Ready()
    {
        TaskContainer = new ItemContainer<TaskItem>(() =>
        {
            return GetNode<InstancePlaceholder>("RightPanel/TaskContainer/TaskItem");
        });

        ToastContainer = new ItemContainer<ToastItem>(() =>
        {
            return GetNode<InstancePlaceholder>("RightPanel/ToastContainer/ToastItem");
        });

        City.Pressed += () =>
        {
            var cityListPanel = LeftPanel.ShowCityListPanel();
        };

        NextTurnStarted += () =>
        {
            NextTurn.Visible = false;

            foreach (var maskPanel in RunMaskPanel.Panels)
            {
                maskPanel.Visible = true;
            }
        };

        NextTurnFinished += () =>
        {
            NextTurn.Visible = true;

            foreach (var maskPanel in RunMaskPanel.Panels)
            {
                maskPanel.Visible = false;
            }
        };
    }
}
