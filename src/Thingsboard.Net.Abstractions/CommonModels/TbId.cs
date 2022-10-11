using System;

namespace Thingsboard.Net;

public class TbId
{
    public Guid Id { get; set; }

    public TbId(Guid id)
    {
        Id = id;
    }

    public TbId()
    {
    }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{nameof(Id)}: {Id}";
    }
}
