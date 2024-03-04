//using Godot;
//using Tais.Commands;
//using Tais.Interfaces;

//public abstract partial class PresentControl<TView> : PresentControl<TView, ISession>
//    where TView : ViewControl
//{
//    protected override void SendCommand(object command)
//    {
//        if (command is not Cmd_UIRefresh)
//        {
//            GD.Print($"OnUICommand:{command.GetType()}");

//            Model.OnCommand(command as ICommand);
//        }

//        foreach (PresentBase item in list)
//        {
//            item.IsDirty = true;
//        }
//    }
//}