using FeedApp.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Interfaces.IServices
{
    public interface ISigninService
    {
        Task<Users> GetUser(string email);
        Task<int> GetUserID(string email);
        Task<Users> Signin( string email, string password);
    }
}
