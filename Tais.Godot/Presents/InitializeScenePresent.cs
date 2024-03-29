using System;
using Tais.InitialDatas.Interfaces;
using Tais.Interfaces;
using Tais.Sessions;
using static System.Collections.Specialized.BitVector32;

internal partial class InitializeScenePresent : PresentControl<InitializeScene, IInitialData>
{
    protected override void Initialize(InitializeScene view, IInitialData model)
    {
        view.Confirm.Pressed += () =>
        {
            var global = GetNode<Initialize>("/root/Global");

            global.session = SessionBuilder.Build(model, global.modder);

            GetTree().ChangeSceneToFile("res://MainScene/MainScene.tscn");
        };
    }

    protected override void Update(InitializeScene view, IInitialData model)
    {
        throw new NotImplementedException();
    }
}
