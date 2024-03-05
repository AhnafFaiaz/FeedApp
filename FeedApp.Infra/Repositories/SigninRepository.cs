using FeedApp.Core.Entities.General;
using FeedApp.Core.Exceptions;
using FeedApp.Core.Interfaces.IRepositories;
using FeedApp.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Infra.Repositories
{
    public class SigninRepository<T> : ISigninRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        protected DbSet<T> DbSet => _dbContext.Set<T>();

        public SigninRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> GetByEmail(string email) 
        {
            //var data = await _dbContext.Set<T>().FindAsync(email);
            var data = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (data == null)
                throw new NotFoundException("No data found");
            return data as T;
        }

        public async Task<T> Signin(string email, string password)
        {
            
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                return user as T;
            }
            else
            {
                return null;
            }
        }

    }
}
