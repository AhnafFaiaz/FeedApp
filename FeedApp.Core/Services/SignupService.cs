using FeedApp.Core.Entities.General;
using FeedApp.Core.Interfaces.IMapper;
using FeedApp.Core.Interfaces.IRepositories;
using FeedApp.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Services
{
    public class SignupService : ISignupService
    {
        private readonly ISignupRepository<Users> _signupRepository;



        public SignupService(ISignupRepository<Users> signupRepository)
        {
            _signupRepository = signupRepository;
           
        }

        //public async Task<Users> GetUser(string email)
        //{
        //    return await _signupRepository.GetByEmail(email);
        //}

        public async Task<Users> Signup(string username, string email, string password)
        {
            //var existingUser = await _signupRepository.GetByEmail(email);
            //existingUser != null &&
            if ( !string.IsNullOrWhiteSpace(username) && !string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password))
            {
                var users = new Users
                {
                    UserName = username,
                    Email = email,
                    Password = password,
                    RoleID = 2 // RoleID 2 for Registered users
                };

                var createdUser = await _signupRepository.Signup(users);
                return createdUser;
            }
            
            else
            {
                throw new Exception("Invalid input parameters for signup.");
            }

        }

        
    }
}
