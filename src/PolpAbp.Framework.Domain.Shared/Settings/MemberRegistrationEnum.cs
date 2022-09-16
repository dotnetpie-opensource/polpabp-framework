using System;
namespace PolpAbp.Framework.Settings
{
    /// <summary>
    /// Determines how a member becomes active in an organization.
    /// </summary>
    public enum MemberRegistrationEnum
    {
        // Email (0), Admin (1), Auto (2) ...
        /// <summary>
        /// Default one
        /// It requires that the system sends out a confirmation email
        /// which in turn a user uses to activate the account.
        /// In this case, no admin is required.
        /// </summary>
        RequireEmailActivation = 0,
        /// <summary>
        /// An admin must approve the user to be active.
        /// So a user has to confirm the email.
        /// </summary>
        RequireAdminApprovel = 10,
        /// <summary>
        /// The admin does not need to approve the user.
        /// The email has to be confirmed.
        /// </summary>
        AutoActive = 20 
    }
}

