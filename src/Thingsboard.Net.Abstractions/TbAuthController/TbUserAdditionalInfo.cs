using System;
using System.Collections.Generic;

namespace Thingsboard.Net.TbAuthController;

public class TbUserAdditionalInfo
{
    public TbUserAdditionalInfo(
        string                     description,
        Guid                       defaultDashboardId,
        bool                       defaultDashboardFullscreen,
        Guid                       homeDashboardId,
        bool                       homeDashboardHideToolbar,
        Dictionary<string, string> userPasswordHistory,
        bool                       userCredentialsEnabled,
        int                        failedLoginAttempts,
        long                       lastLoginTs
    )
    {
        Description                = description;
        DefaultDashboardId         = defaultDashboardId;
        DefaultDashboardFullscreen = defaultDashboardFullscreen;
        HomeDashboardId            = homeDashboardId;
        HomeDashboardHideToolbar   = homeDashboardHideToolbar;
        UserPasswordHistory        = userPasswordHistory;
        UserCredentialsEnabled     = userCredentialsEnabled;
        FailedLoginAttempts        = failedLoginAttempts;
        LastLoginTs                = lastLoginTs;
    }

    public string                     Description                { get; }
    public object                     DefaultDashboardId         { get; }
    public bool                       DefaultDashboardFullscreen { get; }
    public object                     HomeDashboardId            { get; }
    public bool                       HomeDashboardHideToolbar   { get; }
    public Dictionary<string, string> UserPasswordHistory        { get; }
    public bool                       UserCredentialsEnabled     { get; }
    public int                        FailedLoginAttempts        { get; }
    public long                       LastLoginTs                { get; }
}
