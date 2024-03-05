using FeedApp.Core.Interfaces.IServices;
using FluentEmail.Core;
using FluentEmail.Smtp;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using FluentEmail;



namespace FeedApp.Core.Services
{
    

    public class EmailService : IEmailService
    {
        public async Task SendProblemEmailAsync(string userEmail,string adminEmail, string subject, string message)
        {
            var email = new Email(userEmail) // Use the user's email as the recipient
                .To(adminEmail) // Send from admin to admin's email
                .Subject(subject)
                .Body(message);
            await email.SendAsync();

        }

    }

}
