using System;

namespace Thingsboard.Net;

public class TbRpcId
{
    public TbRpcId(Guid rpcId)
    {
        RpcId = rpcId;
    }

    public Guid RpcId { get; }
}
