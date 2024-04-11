using Tais.Interfaces;
using Tais.Modders.Interfaces;
using Tais.ProcessContexts;

namespace Tais.Modders.Conditions;

public class PopMinCountCondition : ICondition
{
    private int minCount;

    public PopMinCountCondition(int minCount)
    {
        this.minCount = minCount;
    }

    public bool IsSatisfied(IProcessContext context)
    {
        if (((ProcessContext)context).current is not IPop pop)
        {
            throw new Exception();
        }

        return pop.Count >= minCount;
    }

    public override string ToString()
    {
        return $"PopCount more or equal {minCount}";
    }
}

public class CentralGovRequestTaxFullFill : ICondition
{
    public bool IsSatisfied(IProcessContext context)
    {
        if (((ProcessContext)context).current is not ICentralGov centralGov)
        {
            throw new Exception();
        }


        return centralGov.RequestTax.CurrValue <= ((ProcessContext)context).session.Finance.ExpectYearReserve;
    }
}

public class CentralGovRequestTaxNotFullFill : ICondition
{
    public bool IsSatisfied(IProcessContext context)
    {
        if (((ProcessContext)context).current is not ICentralGov centralGov)
        {
            throw new Exception();
        }


        return centralGov.RequestTax.CurrValue > ((ProcessContext)context).session.Finance.ExpectYearReserve;
    }
}

//public class IsLess : ICondition
//{
//    private Func<object, float> dataVisitor1;
//    private Func<object, float> dataVisitor2;

//    public IsLess(string p1, string p2)
//    {
//        this.dataVisitor1 = DataVisitBuilder.Build<float>(p1);
//        this.dataVisitor2 = DataVisitBuilder.Build<float>(p2);
//    }

//    public bool IsSatisfied(object target)
//    {
//        return dataVisitor1(target) < dataVisitor2(target);
//    }
//}

//public static class DataVisitBuilder
//{
//    internal static Func<object, T> Build<T>(string param)
//    {
//        throw new NotImplementedException();
//    }
//}