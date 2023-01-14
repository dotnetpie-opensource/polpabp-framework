﻿using System;
using System.Threading.Tasks;

namespace PolpAbp.Framework.Emailing.Account
{
    public interface IFrameworkAccountEmailer
    {
        /// <summary>
        /// Sends out an activation message to the given account, 
        /// in order for the account owner to confirm the existence of the email
        /// or to activate the account depending on the usage of the host.
        /// 
        /// This method is run regardless the current tenant.
        /// 
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <param name="cc">Extra email receipents, separated by ,</param>
        /// <returns>Task</returns>
        Task SendEmailActivationLinkAsync(Guid userId, string cc);

        /// <summary>
        /// Sends out a security message when the password of an account is changed. 
        ///
        /// This method is run regardless the current tenant.
        /// 
        /// This method may put the message into a queue in the background, depending 
        /// on the host configurations.
        /// </summary>
        /// <param name="userId">User Identifier</param>
        /// <param name="cc">Extra email receipents, separated by ,</param>
        /// <returns>Task</returns>
        Task SendPasswordChangeNotyAsync(Guid userId, string cc);

        /// <summary>
        /// Sends out a two factor code.
        ///
        /// This method is run regardless the current tenant.
        /// 
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="code">Two factor code</param>
        /// <param name="cc">Extra email receipents, separated by ,</param>
        /// <returns>Task</returns>
        Task SendTwoFactorCodeAsync(Guid userId, string code, string cc);


        /// <summary>
        /// Sends out a new registration request to the admins of the organization
        /// for the given user.
        /// </summary>
        /// <param name="userId">The new member</param>
        /// <param name="cc">Extra email receipents, separated by ,</param>
        /// <returns>Task</returns>
        Task SendMemberRegistrationNotyAsync(Guid userId, string cc);

        /// <summary>
        /// Sends out an approvel request to the admins of the organization
        /// for the given user.
        /// </summary>
        /// <param name="userId">The new member</param>
        /// <param name="cc">Extra email receipents, separated by ,</param>
        /// <returns>Task</returns>
        Task SendMemberRegistrationApprovalAsync(Guid userId, string cc);

    }
}
