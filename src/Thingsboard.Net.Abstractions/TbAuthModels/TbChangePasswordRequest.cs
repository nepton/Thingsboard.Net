namespace Thingsboard.Net;

public class TbChangePasswordRequest
{
    public TbChangePasswordRequest(
        string currentPassword,
        string newPassword
    )
    {
        this.CurrentPassword = currentPassword;
        this.NewPassword     = newPassword;
    }

    public string CurrentPassword { get; }
    public string NewPassword     { get; }
}