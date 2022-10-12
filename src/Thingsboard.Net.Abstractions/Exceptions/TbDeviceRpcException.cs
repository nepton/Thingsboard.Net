using System;

namespace Thingsboard.Net.Exceptions;

public class TbDeviceRpcException : Exception
{
    public TbDeviceRpcException(TbDeviceRpcErrorCode code)
    {
        ErrorCode = code;
    }

    public TbDeviceRpcErrorCode ErrorCode { get; }
}
