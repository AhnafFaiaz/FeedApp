using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Interfaces.IServices
{
    public interface IEmailService
    {
        Task SendProblemEmailAsync(string userEmail,string adminEmail, string subject, string message);
    }
}
