namespace Tais.Godot.Utilities.UBBCodes;

public abstract class UBBWapper
{
    protected object content;

    protected object wapperValue;

    public UBBWapper(object content)
    {
        this.content = content;
    }

    public UBBWapper Color(UBBColor color)
    {
        return new UBBWapperColor(this) { wapperValue = color };
    }
}

public class UBBWapperColor : UBBWapper
{
    public UBBWapperColor(object content) : base(content)
    {
    }

    public override string ToString()
    {
        return $"[{wapperValue}]{content}[{wapperValue}]";
    }
}