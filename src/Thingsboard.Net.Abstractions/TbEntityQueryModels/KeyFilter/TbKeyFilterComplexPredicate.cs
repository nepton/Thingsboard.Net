using System;
using System.Linq;

namespace Thingsboard.Net;

public class TbKeyFilterComplexPredicate : TbKeyFilterPredicate
{
    public override string Type => "COMPLEX";

    public TbKeyFilterComplexOperation Operation { get; set; }

    /// <summary>
    /// The list of predicates to be evaluated.
    /// </summary>
    public TbKeyFilterPredicate[] Predicates { get; set; } = Array.Empty<TbKeyFilterPredicate>();

    public TbKeyFilterComplexPredicate()
    {
    }

    public TbKeyFilterComplexPredicate(TbKeyFilterComplexOperation operation, params TbKeyFilterPredicate[] predicates)
    {
        Operation  = operation;
        Predicates = predicates;
    }

    public override string ToString()
    {
        return $"{Predicates.FirstOrDefault()?.ToString() ?? "???"} {Operation} {Predicates.Skip(1).FirstOrDefault()?.ToString() ?? "???"}";
    }
}
