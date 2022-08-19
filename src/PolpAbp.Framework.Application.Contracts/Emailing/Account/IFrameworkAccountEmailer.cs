using System;
using System.Threading.Tasks;

namespace PolpAbp.Framework.Emailing.Account
{
    public interface IFrameworkAccountEmailer
    {
        Task SendEmailActivationLinkAsync(string email);
        Task SendPasswordChangeNotyAsync(Guid userId);
    }
}
