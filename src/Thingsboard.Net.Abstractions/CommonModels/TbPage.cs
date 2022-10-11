using System;

namespace Thingsboard.Net;

/// <summary>
/// The paged collection.
/// </summary>
/// <typeparam name="TSource"></typeparam>
public class TbPage<TSource>
{
    /// <summary>
    /// Empty page.
    /// </summary>
    public static readonly TbPage<TSource> Empty = new(0, 0, false, Array.Empty<TSource>());

    /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
    public TbPage(int totalPages, int totalElements, bool hasNext, TSource[] data)
    {
        TotalPages    = totalPages;
        TotalElements = totalElements;
        HasNext       = hasNext;
        Data          = data;
    }

    /// <summary>
    /// How many items are skipped in the current page
    /// </summary>
    public int TotalPages { get; }

    /// <summary>
    /// How many items are taken from the total number of items
    /// </summary>
    public int TotalElements { get; }

    /// <summary>
    /// How many items are in the full collection.
    /// </summary>
    public bool HasNext { get; }

    /// <summary>
    /// The records
    /// </summary>
    public TSource[] Data { get; }
};
