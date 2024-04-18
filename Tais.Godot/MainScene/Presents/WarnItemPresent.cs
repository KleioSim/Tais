using System.Linq;
using Tais.Interfaces;

public partial class WarnItemPresent : PresentControl<WarnItem, ISession>
{
    protected override void Initialize(WarnItem view, ISession model)
    {
        var warnObj = view.Id as IWarn;
        view.TooltipTrigger.funcGetToolTipString = () =>
        {
            return string.Join("\n", warnObj.Items);
        };
    }

    protected override void Update(WarnItem view, ISession model)
    {
        var warnObj = view.Id as IWarn;

        view.CountPanel.Visible = warnObj.Items.Count() > 1;
        view.Count.Text = warnObj.Items.Count().ToString();
    }
}