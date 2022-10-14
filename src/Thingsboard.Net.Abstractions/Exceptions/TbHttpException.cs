using System;
using System.Net;

namespace Thingsboard.Net.Exceptions;

public class TbHttpException : TbException
{
    public TbHttpException(string message, bool completed, HttpStatusCode? statusCode, DateTime timestamp, int errorCode) : base(message)
    {
        Completed  = completed;
        StatusCode = statusCode;
        Timestamp  = timestamp;
        ErrorCode  = errorCode;
    }

    public bool Completed { get; }

    public int ErrorCode { get; }

    public DateTime Timestamp { get; }

    public HttpStatusCode? StatusCode { get; }

    /// <summary>Creates and returns a string representation of the current exception.</summary>
    /// <returns>A string representation of the current exception.</returns>
    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(Completed)}: {Completed}, {nameof(Timestamp)}: {Timestamp}, {nameof(StatusCode)}: {StatusCode}";
    }
}
