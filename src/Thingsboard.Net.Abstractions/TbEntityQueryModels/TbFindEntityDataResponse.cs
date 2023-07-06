using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Thingsboard.Net;

public class TbFindEntityDataResponse
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object"></see> class.</summary>
    public TbFindEntityDataResponse(
        TbEntityId                                                               entityId,
        ReadOnlyDictionary<string, ReadOnlyDictionary<string, TbEntityTsValue>>? latest,
        ReadOnlyDictionary<string, TbEntityTsValue[]>?                           timeseries)
    {
        EntityId   = entityId;
        Latest     = ConvertLatest(latest);
        Timeseries = timeseries;
    }

    private ReadOnlyDictionary<TbEntityField, TbEntityTsValue>? ConvertLatest(ReadOnlyDictionary<string, ReadOnlyDictionary<string, TbEntityTsValue>>? latest)
    {
        if (latest == null)
            return null;

        var q = from l in latest
                from v in l.Value
                select new
                {
                    Field = new TbEntityField(v.Key, (TbEntityFieldType) Enum.Parse(typeof(TbEntityFieldType), l.Key)),
                    Value = v.Value,
                };

        return new ReadOnlyDictionary<TbEntityField, TbEntityTsValue>(q.ToDictionary(x => x.Field, x => x.Value));
    }

    public TbEntityId EntityId { get; }

    public ReadOnlyDictionary<TbEntityField, TbEntityTsValue>? Latest { get; }

    public ReadOnlyDictionary<string, TbEntityTsValue[]>? Timeseries { get; }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return EntityId.ToString();
    }
}
