using System;

namespace Thingsboard.Net;

public class TbEntity
{
    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbEntity(TbEntityType type, Guid id, TbEntityValue[] latest)
    {
        Type         = type;
        Id           = id;
        LatestValues = latest;
    }

    public TbEntityType Type { get; }

    public Guid Id { get; }

    public TbEntityValue[] LatestValues { get; }

    /// <summary>Returns a string that represents the current object.</summary>
    /// <returns>A string that represents the current object.</returns>
    public override string ToString()
    {
        return $"{nameof(Type)}: {Type}, {nameof(Id)}: {Id}";
    }
}
