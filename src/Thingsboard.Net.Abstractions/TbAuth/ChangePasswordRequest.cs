namespace Thingsboard.Net.TbAuth;

public class ChangePasswordRequest
{
    public ChangePasswordRequest(
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