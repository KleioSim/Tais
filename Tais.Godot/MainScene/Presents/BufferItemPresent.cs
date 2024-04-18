using System.Linq;
using Tais.Interfaces;

public partial class BufferItemPresent : PresentControl<BufferItem, ISession>
{
    protected override void Initialize(BufferItem view, ISession model)
    {
        view.ToolTipTrigger.funcGetToolTipString = () =>
        {
            var buffObj = view.Id as IBuffer;

            return buffObj.Name + "\n" + string.Join("\n", buffObj.Effects.Select(x => $"{x.GetType().Name}  {x.Percent.ToString("+0%;-#%")}"));
        };
    }

    protected override void Update(BufferItem view, ISession model)
    {

    }
}
