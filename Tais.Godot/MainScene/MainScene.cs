﻿using Godot;
using System;

public partial class MainScene : ViewControl
{
    public static Action OnGMFailed;
    public static Action OnGMSuccessed;

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

    internal Control ConsolePanel => GetNode<Control>("ConsolePanel");

    internal ItemContainer<TaskItem> TaskContainer;
    internal ItemContainer<ToastItem> ToastContainer;
    internal ItemContainer<WarnItem> WarnContainer;

    internal GMFailedEvent GMFailedEvent => GetNode<GMFailedEvent>("GMFailedEvent");

    public override void _EnterTree()
    {
        base._EnterTree();

        CommandConsole.IsVaild = true;
    }

    public override void _ExitTree()
    {
        base._ExitTree();

        CommandConsole.IsVaild = false;
    }

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

        WarnContainer = new ItemContainer<WarnItem>(() =>
        {
            return GetNode<InstancePlaceholder>("TopInfo/Warns/WarnItem");
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

        GMFailedEvent.Confirm.Pressed += () =>
        {
            OnGMFailed?.Invoke();
        };
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        if (Input.IsActionJustReleased("cheat_switch"))
        {
            ConsolePanel.Visible = !ConsolePanel.Visible;
        }
    }
}
