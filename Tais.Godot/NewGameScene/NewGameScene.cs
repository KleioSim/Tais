using Godot;
using System;

public partial class NewGameScene : Control
{
    public static Action OnEnterGame { get; set; }
    public static Action OnBack { get; set; }
}
