using System;

namespace Thingsboard.Sdk.Exceptions;

public class TbDeviceRpcException : Exception
{
    public TbDeviceRpcException(TbDeviceRpcErrorCode code)
    {
        ErrorCode = code;
    }

    public TbDeviceRpcErrorCode ErrorCode { get; }
}