﻿using Tais.Interfaces;

public partial class DebugSessionPresent : PresentControl<DebugSession, ISession>
{
    protected override void Initialize(DebugSession view, ISession model)
    {
        GetTree().ChangeSceneToFile("res://MainScene/MainScene.tscn");
    }

    protected override void Update(DebugSession view, ISession model)
    {

    }
}