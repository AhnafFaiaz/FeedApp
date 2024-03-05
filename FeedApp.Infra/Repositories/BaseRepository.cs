using FeedApp.Core.Entities.Business;
using FeedApp.Core.Entities.General;
using FeedApp.Core.Enum;
using FeedApp.Core.Exceptions;
using FeedApp.Core.Interfaces.IRepositories;
using FeedApp.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Infra.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;
        protected DbSet<T> DbSet => _dbContext.Set<T>();

        public BaseRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Create(T model)
        {
            await _dbContext.Set<T>().AddAsync(model);
            await _dbContext.SaveChangesAsync();
            return model;
        }

        public async Task Delete(T model)
        {
            _dbContext.Set<T>().Remove(model);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<Problems> GetById(int id)//id dhukbe problem er jetar status change korbo
        {
            var data = await _dbContext.Set<Problems>().FindAsync(id);
            if (data == null)
                throw new NotFoundException("No data found");
            return data;
        }



        public virtual async Task<PaginatedDataViewModel<T>> GetPaginatedData( int pageNumber, int pageSize)
        {
            var query = _dbContext.Set<T>()//Shob data eshe portese
                                           //Include(p=>p.Users.Username)//Convertion error
                 
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();

            var data = await query.ToListAsync();
            var totalCount = await _dbContext.Set<T>().CountAsync();/* CountAsync(item => item.UserID == userId);*/

            return new PaginatedDataViewModel<T>(data, totalCount);

            //to get the username****
            //var query = _dbContext.Problems
            //.Include(p => p.Users)
            //.Skip((pageNumber - 1) * pageSize)
            //.Take(pageSize)
            //.AsNoTracking();

            //var data = await query.Select(p => new ProblemViewModel
            //{
            //    Id = p.Id,
            //    Subject = p.Subject,
            //    ProblemDesc = p.ProblemDesc,
            //    Status = p.Status,
            //    UserName = p.UserName
            //}).ToListAsync();


            //var totalCount = await _dbContext.Set<T>().CountAsync();

            //return new PaginatedDataViewModel<T>(data, totalCount);
        }

        public async Task SaveChangeAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(Problems problems)
        {
            //_dbContext.Set<T>().Update(model);
            //await _dbContext.SaveChangesAsync();
            _dbContext.Problems.Update(problems); // Attach the entity to the context
            _dbContext.Entry(problems).Property(p => p.Status).IsModified = true; // Mark Status property as modified
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Problems>> GetProblemsById(int id)
        {
            var data = await _dbContext.Set<Problems>().Where(p => p.UserId == id).ToListAsync();

            if (data == null)
                throw new NotFoundException("No data found");
            return data;
        }

        public async Task<IEnumerable<Reply>> GetRepliesForProblemId(int id)
        {
            //Working
            var data = await _dbContext.Set<Reply>()
                .Where(p => p.ProblemId == id)
                .OrderByDescending(p => p.EntryDate)
                .Include(p => p.Users)
                .Include(r => r.Problems)
                .ToListAsync()
                ;

            if (data == null)
                throw new NotFoundException("No data found");
                return data;
            //    var replies = await _dbContext.Reply
            //.Where(r => r.ProblemId == id)
            //.ToListAsync();

            //    // Assuming you have access to the Problems DbSet
            //    var problems = await _dbContext.Problems
            //        .Where(p => p.Id == id)
            //        .ToListAsync();

            //    foreach (var reply in replies)
            //    {
            //        reply.Problems = problems.SingleOrDefault(p => p.Id == reply.ProblemId);
            //    }

            //    return replies;
        }

        public async Task<Reply> AddReplyAsync(Reply reply, int problemId, Status status,int userid)
        {
            reply.UserId = userid;//FK_Clash,Tai add korsi shob space e
            reply.Status = status;//Status dhuktese na dekhe add korsi
            reply.ProblemId = problemId;//Ekhane problemId dhuktese na dekhe add korsi
            await _dbContext.Set<Reply>().AddAsync(reply);
            await _dbContext.SaveChangesAsync(); //Add reply te FK problem ekhane occur kore
            return reply;
        }

        //public Task<T> GetById(int id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<IEnumerable<Reply>> GetRepliesById(int userId)
        {
            //var data = await _dbContext.Set<Reply>().Where(p => p.ProblemId= problemId).ToListAsync();

            //if (data == null)
            //    throw new NotFoundException("No data found");
            //return data;
            return await _dbContext.Reply
            .Include(r => r.Problems) // Include the associated Problem
            .OrderByDescending(p => p.EntryDate)
            .Where(r => r.Problems.UserId == userId) // Filter by user's problems
            .ToListAsync();
        }

        //public async Task<PaginatedDataViewModel<T>> GetPaginatedDataByUserID(int id, int pageNumber, int pageSize)
        //{
        //    var query = _dbContext.Set<Problems>()//Shob data eshe portese
        //        .Where(r => r.UserID == id)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .AsNoTracking();

        //    var data = await query.ToListAsync();
        //    var totalCount = await _dbContext.Set<Problems>().CountAsync();/* CountAsync(item => item.UserID == userId);*/

        //    return new PaginatedDataViewModel<T>(data, totalCount);

        //}
    }
}
