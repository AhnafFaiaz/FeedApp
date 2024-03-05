using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Interfaces.IRepositories
{
    public interface ISigninRepository<T> where T : class
    {
        Task<T> GetByEmail (string email);
        Task<T> Signin(string email, string password);
    }
}
