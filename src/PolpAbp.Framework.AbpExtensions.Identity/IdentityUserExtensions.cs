using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace Volo.Abp.Identity
{
    // We cannot have the identity extensions in the general abp extensions.
    // Otherwise, we will have trouble with the unit tests. 
    // The identity module contains the data seeder for roles, ....
    public static class IdentityUserExtensions
    {
        public const string ShouldChangePasswordOnNextLoginPropKey = "ShouldChangePasswordOnNextLogin";

        /// <summary>
        /// Tells whether the user should change the password on next login.
        /// </summary>
        /// <param name="user">User</param>
        /// <returns>True/false</returns>
        public static bool ShouldChangePasswordOnNextLogin(this IdentityUser user)
        {
            return user.GetProperty(ShouldChangePasswordOnNextLoginPropKey, false);
        }

        /// <summary>
        /// Sets the property to indicate that the user should 
        /// have his/her password changed on next login.
        /// 
        /// This method should be followed by the update method from 
        /// the identity manager.
        /// </summary>
        /// <param name="user">User</param>
        public static void SetShouldChangePasswordOnNextLogin(this IdentityUser user)
        {
            user.SetProperty(ShouldChangePasswordOnNextLoginPropKey, true);
        }

        /// <summary>
        /// Remove the property to indicate that the user should not
        /// have his/her password changed on next login.
        /// 
        /// This method should be followed by the update method from 
        /// the identity manager.
        /// </summary>
        /// <param name="user">User</param>
        public static void RemoveShouldChangePasswordOnNextLogin(this IdentityUser user)
        {
            user.RemoveProperty(ShouldChangePasswordOnNextLoginPropKey);
        }
    }
}
