using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Interfaces.IRepositories
{
    public interface ISignupRepository<Users> where Users : class
    {
        //Task<Users> GetByEmail(string email);
        Task<Users> Signup(Users model);

    }
}
