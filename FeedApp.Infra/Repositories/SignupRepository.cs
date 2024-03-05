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
    public class SignupRepository<Users> : ISignupRepository<Users> where Users : class
    {
        private readonly ApplicationDbContext _dbContext;
        protected DbSet<Users> DbSet => _dbContext.Set<Users>();

        public SignupRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public async Task<T> GetByEmail(string email)
        //{
        //    //var data = await _dbContext.Set<T>().FindAsync(email);
        //    var data = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        //    if (data == null)
        //        throw new NotFoundException("No data found");
        //    return data as T;
        //}
        //public async Task<Users> GetByEmail(string email)
        //{
        //    var data = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        //    // If no user is found, throw an exception or return null based on your requirement
        //    if (data == null)
        //    {
        //        // You can throw a custom exception here or return null, depending on your design
        //        //throw new NotFoundException("No user found with this email.");

        //    }

        //    return data as Users;
        //}

        public async Task<Users> Signup(Users model)
        {
            var entity = await DbSet.AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return entity.Entity;
        }

        
    }
}
