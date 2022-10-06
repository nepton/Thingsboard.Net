namespace Thingsboard.Net.Utility;

public class TbClientApiOptions
{
    public static readonly TbClientApiOptions Default = new();

    /// <summary>
    /// 是否需要登录
    /// </summary>
    public bool LoginRequired { get; set; } = true;

    public TbCredentials? Credentials  { get; set; }
    public int?           TimeoutInSec { get; set; }
}
