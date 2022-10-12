#nullable enable
using System;

namespace Thingsboard.Net.Exceptions;

public class TbException : Exception
{
    public TbException(string message) : base(message)
    {
    }
}
