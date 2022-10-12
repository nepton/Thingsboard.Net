namespace Thingsboard.Net;

/// <summary>
/// The logger callback interface.
/// </summary>
public interface ITbClientLogger
{
    /// <summary>
    /// When the client has been called, this method will be called.
    /// </summary>
    /// <param name="call"></param>
    void OnLogging(TbClientCall call);

    /// <summary>
    /// Indicates whether the logger is enabled.
    /// </summary>
    bool IsEnabled { get; }
}
