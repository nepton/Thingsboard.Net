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
}
