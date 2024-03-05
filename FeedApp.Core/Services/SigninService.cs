using FeedApp.Core.Entities.General;
using FeedApp.Core.Interfaces.IRepositories;
using FeedApp.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Services
{
    public class SigninService : ISigninService
    {

        private readonly ISigninRepository<Users> _signinRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SigninService(ISigninRepository<Users> signinRepository, IHttpContextAccessor httpContextAccessor)
        {
            _signinRepository = signinRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Users> GetUser(string email)
        {
            return await _signinRepository.GetByEmail(email);
        }

      
        public async Task<Users> Signin(string email, string password)
        {
            var user = await _signinRepository.Signin(email,password);

            if (user !=null)
            {
                //var session = _httpContextAccessor.HttpContext.Session;
                //var userEmailBytes = System.Text.Encoding.UTF8.GetBytes(email);
                //session.Set("UserEmail", userEmailBytes);
                
                var ses = System.Text.Encoding.UTF8.GetBytes(user.UserID.ToString());
                _httpContextAccessor.HttpContext.Session.Set("UserID", ses);
                //_httpContextAccessor.HttpContext.Session.SetString("UserEmail", email);
                return user;
            }
            else
            {
                throw new Exception("Invalid email or password."); //etar jonne sigin e again jay na
            }
        }

         async Task<int> ISigninService.GetUserID(string email)
        {
            var user = await _signinRepository.GetByEmail(email);

            if (user != null)
            {
                return user.UserID; 
            }
            return 0;
        }
    }
}
