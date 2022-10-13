using System;

namespace Thingsboard.Net.Exceptions;

public class TbOptionsException : Exception
{
    public string OptionName { get; }

    public TbOptionsException(string optionName, string message) : base(message)
    {
        OptionName = optionName;
    }
}
