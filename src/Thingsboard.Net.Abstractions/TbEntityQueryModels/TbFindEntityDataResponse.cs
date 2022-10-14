using System;
using System.Collections.ObjectModel;

namespace Thingsboard.Net;

public class TbFindEntityDataResponse
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
    public TbFindEntityDataResponse(TbEntityId                                     entityId,
        ReadOnlyDictionary<string, ReadOnlyDictionary<string, TbEntityTimeValue>>? latest,
        ReadOnlyDictionary<string, TbEntityTimeValue[]>?                           timeseries)
    {
        EntityId   = entityId;
        Latest     = latest;
        Timeseries = timeseries;
    }

    public TbEntityId EntityId { get; }

    public ReadOnlyDictionary<string, ReadOnlyDictionary<string, TbEntityTimeValue>>? Latest { get; }

    public ReadOnlyDictionary<string, TbEntityTimeValue[]>? Timeseries { get; }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return EntityId.ToString();
    }
}
