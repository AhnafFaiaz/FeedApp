using FeedApp.Core.Entities.Business;
using FeedApp.Core.Entities.General;
using FeedApp.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Interfaces.IRepositories
{
    //Unit of Work Pattern
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<PaginatedDataViewModel<T>> GetPaginatedData(int pageNumber, int pageSize);
        //Task<PaginatedDataViewModel<T>> GetPaginatedDataByUserID(int id, int pageNumber, int pageSize);
        //Task<T> GetById<Tid>(Tid id);
        //Task<T> GetById(int id);
        Task<Problems> GetById(int id);
        Task<IEnumerable<Problems>> GetProblemsById(int id);
        Task<IEnumerable<Reply>> GetRepliesForProblemId(int id);
        Task<T> Create(T model);
        Task Update(Problems problems);
        Task Delete(T model);
        Task SaveChangeAsync();
        Task<Reply> AddReplyAsync(Reply reply, int problemId, Status status, int userid);
        Task<IEnumerable<Reply>> GetRepliesById(int userId);
    }
}
