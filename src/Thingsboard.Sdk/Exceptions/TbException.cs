#nullable enable
using System;

namespace Thingsboard.Sdk.Exceptions;

public class TbException : Exception
{
    public TbException(string message) : base(message)
    {
    }
}
