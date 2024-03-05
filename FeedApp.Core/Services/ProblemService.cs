using FeedApp.Core.Entities.Business;
using FeedApp.Core.Entities.General;
using FeedApp.Core.Interfaces.IMapper;
using FeedApp.Core.Interfaces.IServices;
using FeedApp.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using AutoMapper;
using FeedApp.Core.Enum;
using FeedApp.Core.Exceptions;
using System.Reflection;

namespace FeedApp.Core.Services
{
    public class ProblemService : IProblemService
    {
        private readonly IBaseMapper<Problems, ProblemViewModel> _problemViewModelMapper;
        private readonly IBaseMapper<ProblemViewModel, Problems> _problemsMapper;
        private readonly IProblemRepository _problemRepository;
        private readonly ISigninService _signinService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        


        public ProblemService (IBaseMapper<Problems, ProblemViewModel> problemViewModelMapper,
            IBaseMapper<ProblemViewModel, Problems> problemsMapper,
            IProblemRepository problemRepository,
            ISigninService signinService,
            IHttpContextAccessor httpContextAccessor)
        {
            _problemsMapper = problemsMapper;
            _problemViewModelMapper = problemViewModelMapper;
            _problemRepository = problemRepository;
            _signinService = signinService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<PaginatedDataViewModel<ProblemViewModel>> GetPaginatedProblems(int pageNumber, int pageSize)
        {
            var paginatedData = await _problemRepository.GetPaginatedData(pageNumber, pageSize);

            //Map data with ViewModel
            var mappedData = _problemViewModelMapper.MapList(paginatedData.Data);

            var paginatedDataViewModel = new PaginatedDataViewModel<ProblemViewModel>(mappedData.ToList(), paginatedData.TotalCount);

            return paginatedDataViewModel;
        }

        public async Task<PaginatedDataViewModel<ProblemViewModel>> GetPaginatedProblemsByUserId(int userId, int pageNumber, int pageSize)
        {
            var paginatedData = await _problemRepository.GetPaginatedData(pageNumber, pageSize);

            //Map data with ViewModel
            var mappedData = _problemViewModelMapper.MapList(paginatedData.Data);

            var paginatedDataViewModel = new PaginatedDataViewModel<ProblemViewModel>(mappedData.ToList(), paginatedData.TotalCount);

            return paginatedDataViewModel;
        }

        public async Task<IEnumerable<ProblemViewModel>> GetProblems()
        {
            return _problemViewModelMapper.MapList(await _problemRepository.GetAll());
        }

        public async Task<IEnumerable<ProblemViewModel>> GetProblem(int id)
        {
            //return _problemViewModelMapper.MapModel(await _problemRepository.GetById(id));
            var problems = await _problemRepository.GetProblemsById(id);
            return _problemViewModelMapper.MapList(problems);
        }
        //status er jonne kaj korlam
        public async Task<Problems> GetProblemByProblemId(int problemId) 
        {
            return await _problemRepository.GetById(problemId);
        }

        public async Task<IEnumerable<Reply>> GetRepliesForProblem(int Id)
        {
            // Assuming you have a repository to fetch replies by problem ID
            //Working
            var replies = await _problemRepository.GetRepliesForProblemId(Id);


            return replies;
            //var replies = await _problemRepository.GetRepliesForProblemId(Id);

            //foreach (var reply in replies)
            //{
            //    // Assuming you have a method in your repository or service to fetch ProblemDesc
            //    reply.Problems = await _problemRepository.GetById(Id);
            //}

            //return replies;
        }


        public async Task<ProblemViewModel> Create(ProblemViewModel model)
        {
            //_httpContextAccessor.HttpContext.Session.Get();
            //var user = await _signinService.GetUserID();
            //var userIdBytes = _httpContextAccessor.HttpContext.Session.Keys;
            //var userId = userIdBytes != null
            //    ? int.Parse(System.Text.Encoding.UTF8.GetString(userIdBytes))
            //    : 0;
            var entity = _problemsMapper.MapModel(model);
            entity.EntryDate = DateTime.Now;
            entity.UserId = model.UserID;
            

            return _problemViewModelMapper.MapModel(await _problemRepository.Create(entity));
        }
        public async Task Delete(int id)
        {
            //var entity = await _problemRepository.GetProblemsById(id);
            //await _problemRepository.Delete(entity);
        }

        public async Task Update(ProblemViewModel model)
        {
            //var existingData = await _problemRepository.GetProblemsById(model.problemID);

            ////Manual mapping
            //existingData.problemDesc = model.problemDesc;
            ////existingData.isSolved = model.isSolved;
            //existingData.UpdateDate = DateTime.Now;

            //await _problemRepository.Update(existingData);
        }

        public async Task AddReplyAsync(int problemId, string ReplyDesc, Status status,int userid)
        {

            var reply = new Reply
            {
                UserId=userid,
                Status= status,
                ProblemId = problemId,
                ReplyDesc = ReplyDesc,
                EntryDate = DateTime.Now
            };

            await _problemRepository.AddReplyAsync(reply,problemId,status,userid);
        }

        public async Task <IEnumerable<Reply>> GetUserProblemReplies(int userId)
        {
            var replies = await _problemRepository.GetRepliesById(userId);

            return replies;
        }
        public async Task UpdateStatus(int Id, Status newStatus)
        {
            var problem = await _problemRepository.GetById(Id);
            problem.Status = newStatus;

            if (problem == null)
            {
                throw new NotFoundException("Problem not found.");
            }
            //problem = new Problems
            //{
            //    Status = newStatus,
            //};


            await _problemRepository.Update(problem);
        }


    }
}
