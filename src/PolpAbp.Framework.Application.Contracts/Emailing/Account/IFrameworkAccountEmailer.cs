using System;
using System.Threading.Tasks;

namespace PolpAbp.Framework.Emailing.Account
{
    public interface IFrameworkAccountEmailer
    {
        /// <summary>
        /// Sends out the activation email to confirm that the email exists
        /// or to activate the account depending on the usage of the host.
        /// </summary>
        /// <param name="email">Account email</param>
        /// <returns>Task</returns>
        Task SendEmailActivationLinkAsync(string email);
        Task SendPasswordChangeNotyAsync(Guid userId);
    }
}
