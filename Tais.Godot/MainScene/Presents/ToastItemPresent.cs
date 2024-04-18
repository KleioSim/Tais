using Tais.Interfaces;

public partial class ToastItemPresent : PresentControl<ToastItem, ISession>
{
    protected override void Initialize(ToastItem view, ISession model)
    {

    }

    protected override void Update(ToastItem view, ISession model)
    {
        var toast = view.Id as IToast;

        view.Desc.Text = toast.Desc;
    }
}