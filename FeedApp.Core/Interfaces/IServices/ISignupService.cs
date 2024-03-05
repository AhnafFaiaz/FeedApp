using FeedApp.Core.Entities.Business;
using FeedApp.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Interfaces.IServices
{
    public interface ISignupService
    {
        //Task<Users> Signup();
        //Task<Users> GetUser(string email);
        Task<Users> Signup(string username, string email, string password);
    }
}
