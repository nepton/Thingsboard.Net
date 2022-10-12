namespace Thingsboard.Net;

/// <summary>
/// Filter Predicate defines the logical expression to evaluate. The list of available operations depends on the filter value type, see above.
/// Platform supports 4 predicate types: 'STRING', 'NUMERIC', 'BOOLEAN' and 'COMPLEX'.
/// The last one allows to combine multiple operations over one filter key.
/// </summary>
public abstract class TbKeyFilterPredicate
{
    public abstract string Type {get;}
}
