using System.Linq;

namespace Thingsboard.Net.TbEntityQuery;

public class TbKeyFilterComplexPredicate : TbKeyFilterPredicate
{
    public TbKeyFilterComplexOperation Operation { get; }

    /// <summary>
    /// The list of predicates to be evaluated.
    /// </summary>
    public TbKeyFilterPredicate[] Predicates { get; }

    public TbKeyFilterComplexPredicate(TbKeyFilterComplexOperation operation, params TbKeyFilterPredicate[] predicates)
    {
        Operation  = operation;
        Predicates = predicates;
    }

    public override object ToQuery()
    {
        return new
        {
            type       = "COMPLEX",
            operation  = Operation,
            predicates = Predicates.Select(p => p.ToQuery()).ToArray()
        };
    }
}
