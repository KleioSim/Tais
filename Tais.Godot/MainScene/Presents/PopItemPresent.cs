using System.Linq;
using Tais.Interfaces;

public partial class PopItemPresent : PresentControl<PopItem, ISession>
{
    protected override void Initialize(PopItem view, ISession model)
    {

    }

    protected override void Update(PopItem view, ISession model)
    {
        var popObj = view.Id as IPop;

        view.Button.Disabled = !popObj.IsRegisted;

        view.PopName.Text = popObj.Name;
        view.PopCount.Text = popObj.Count.ToString();
        if (!popObj.IsRegisted)
        {
            view.PopCount.Text = string.Join("", Enumerable.Range(0, view.PopCount.Text.Length).Select(_ => "*"));
        }

        if (popObj.Family == null)
        {
            view.FamilyName.Visible = false;
            view.Attitude.Visible = false;
        }
        else
        {
            view.FamilyName.Visible = true;
            view.FamilyName.Text = popObj.Family.Name;

            view.Attitude.Visible = true;
            view.AttitudeLabel.Text = popObj.Family.Attitude.Current.ToString();
        }

        if (popObj.Living == null)
        {
            view.Living.Visible = false;
        }
        else
        {
            view.Living.Visible = true;
            view.LivingLabel.Text = popObj.Living.Current.ToString();
        }
    }
}
