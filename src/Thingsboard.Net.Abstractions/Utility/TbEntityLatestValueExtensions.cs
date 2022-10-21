using System.Collections.Generic;
using System.Linq;

namespace Thingsboard.Net;

public static class TbEntityLatestValueExtensions
{
    public static TbEntityLatestValue? GetLatestValue(this IEnumerable<TbEntityLatestValue>? source, TbEntityField key)
    {
        if (source == null)
            return null;

        return source.FirstOrDefault(x => x.Key == key.Key);
    }
}
