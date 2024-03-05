using FeedApp.Core.Entities.Business;
using FeedApp.Core.Entities.General;
using FeedApp.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Interfaces.IServices
{
    public interface IProblemService
    {
        Task<IEnumerable<ProblemViewModel>> GetProblems();
        Task<PaginatedDataViewModel<ProblemViewModel>> GetPaginatedProblems(int pageNumber, int pageSize);
        Task<PaginatedDataViewModel<ProblemViewModel>> GetPaginatedProblemsByUserId(int userId, int pageNumber, int pageSize);
        Task<IEnumerable<ProblemViewModel>> GetProblem(int id);
        //ekhane status er kaj
        Task <Problems> GetProblemByProblemId(int problemId);
        Task<IEnumerable<Reply>> GetRepliesForProblem(int Id);
        Task<ProblemViewModel> Create(ProblemViewModel model);
        Task Update(ProblemViewModel model);
        Task Delete(int id);
        Task AddReplyAsync(int problemId, string ReplyDesc, Status status, int userid);
        Task<IEnumerable<Reply>> GetUserProblemReplies(int userId);
        Task UpdateStatus(int Id, Status newStatus);
    }
}
