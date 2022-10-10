namespace Thingsboard.Net;

/// <summary>
/// Key Filter allows you to define complex logical expressions over entity field, attribute or latest time-series value.
/// The filter is defined using 'key', 'valueType' and 'predicate' objects. Single Entity Query may have zero, one or multiple predicates.
/// If multiple filters are defined, they are evaluated using logical 'AND'. The example below checks that temperature of the entity is above 20 degrees:
/// </summary>
public class TbKeyFilter
{
    /// <summary>
    /// The key is a string that defines the entity field, attribute or latest time-series value to be filtered.
    /// The following keys are supported:
    /// </summary>
    public TbEntityField? Key { get; set;}

    /// <summary>
    /// The value type defines the type of the value to be filtered. The following value types are supported:
    /// </summary>
    public TbEntityKeyFilterValueType ValueType { get; set;}

    /// <summary>
    /// The predicate defines the filtering operation to be performed. The following predicates are supported:
    /// </summary>
    public TbKeyFilterPredicate? Predicate { get; set;}

    public TbKeyFilter()
    {
    }

    public TbKeyFilter(TbEntityField? key, TbEntityKeyFilterValueType valueType, TbKeyFilterPredicate? predicate)
    {
        Key       = key;
        ValueType = valueType;
        Predicate = predicate;
    }
}
