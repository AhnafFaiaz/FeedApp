using FeedApp.Core.Entities.Business;
using FeedApp.Core.Entities.General;
using FeedApp.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using X.PagedList;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Data;
using System.Collections.Generic;
using FeedApp.Core.Enum;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FeedApp.UI.Controllers
{
    //[Authorize]
    public class ProblemController : Controller
    {
        private readonly ILogger<ProblemController> _logger;
        private readonly IProblemService _problemService;
        private readonly IEmailService _emailService;
        public ProblemController(ILogger<ProblemController> logger, IProblemService problemService, IEmailService emailService)
        {
            _logger = logger;
            _problemService = problemService;
            _emailService = emailService;
        }

        [Authorize(Roles="Admin")]
        public async Task<IActionResult> ProblemIndex(int? page)
        {
            //if (!HttpContext.User.Identity.IsAuthenticated)
            //{
            //    // Redirect to the Signin page if the user is not authenticated
            //    return RedirectToAction("Signin", "Signin");
            //}

            try
            {
                int pageSize = 4;
                int pageNumber = (page ?? 1);

                //Get peginated data
                var problems = await _problemService.GetPaginatedProblems(pageNumber, pageSize);

                // Convert the list of products to an instance of StaticPagedList<ProductViewModel>>
                var pagedProblems = new StaticPagedList<ProblemViewModel>(problems.Data, pageNumber, pageSize, problems.TotalCount);

                return View(pagedProblems);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving problems");
                return StatusCode(500, ex.Message);
            }

        }

        [Authorize(Roles="User")]
        public async Task<IActionResult> MyProblems()
        {
            //if (!HttpContext.User.Identity.IsAuthenticated)
            //{
            //    // Redirect to the Signin page if the user is not authenticated
            //    return RedirectToAction("Signin", "Signin");
            //}
            //int pageNumber = page ?? 1;
            //int pageSize = 4;

            //// Get the current user's ID from session
            //var currentUserId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));

            //// Assuming _problemService.GetProblem returns a ProblemViewModel
            //var userProblem = await _problemService.GetProblem(currentUserId);

            //// Assuming _problemService.GetPaginatedProblems returns an IPagedList<ProblemViewModel>
            //var userProblems = await _problemService.GetPaginatedProblemsByUserId(currentUserId, pageNumber, pageSize);
            //var pagedUserProblems = new StaticPagedList<ProblemViewModel>(userProblems.Data, pageNumber, pageSize, userProblems.TotalCount);

            //return View(pagedUserProblems);

            var userIdBytes = HttpContext.Session.GetString("UserID");
            var userIdInt = Convert.ToInt32(userIdBytes);
            int currentUserId = userIdInt;
            var problem = await _problemService.GetProblem(currentUserId);
            return View(problem);
        }



        [HttpGet]
        [Authorize(Roles="User")]
        public ActionResult Create()
        {

            var st = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
            ViewBag.St = new SelectList(st);
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Create(ProblemViewModel model)
        {
                try
                {
                    //var df = HttpContext.Session.Get("UserID");
                    //model.UserID = Convert.ToInt32(df);

                    model.Status = Status.Awaiting;
                    //model.UserName= HttpContext.Session.GetString("UserName");
                    var userIdBytes = HttpContext.Session.GetString("UserID");
                    if (userIdBytes != null)
                    {
                        //var userIdString = System.Text.Encoding.UTF8.GetString(userIdBytes);
                        var userIdInt = Convert.ToInt32(userIdBytes);
                        //if (int.TryParse(userIdString, out int userId))
                        {
                            
                            model.UserID = userIdInt;
                            var data = await _problemService.Create(model);
                            string adminEmail = "feedapp12@gmail.com"; 
                            string subject = "New Problem Created";
                            string message = $"A new problem was created: {model.Subject}";
                            string userEmail = "systemfeedapp@gmail.com";

                            await _emailService.SendProblemEmailAsync(userEmail, adminEmail, subject, message);
                          
                            return RedirectToAction("MyProblems", "Problem");
                        }
                    }
                    //var data = await _problemService.Create(model);
                    //return RedirectToAction("UserDashboard","Dashboard");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while adding the problem");
                    ModelState.AddModelError("Error", $"An error occurred while adding the problem- " + ex.Message);
                    return View(model);
                }
            
            var st = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
            ViewBag.st = new SelectList(st);
            return View(model);
        }

        //Haven't use
        public async Task<IActionResult> Edit(int problemid)
        {
            try
            {
                var data = await _problemService.GetProblem(problemid);
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the product");
                return StatusCode(500, ex.Message);
            }
        }

        //Haven't use
        [HttpPost]
        public async Task<IActionResult> Edit(ProblemViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _problemService.Update(model);
                    return RedirectToAction("UserDashboard", "Dashboard");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"An error occurred while updating the product");
                    ModelState.AddModelError("Error", $"An error occurred while updating the product- " + ex.Message);
                    return View(model);
                }
            }
            return View(model);
        }

        //Haven't use
        public async Task<IActionResult> Delete(int problemid)
        {
            try
            {
                await _problemService.Delete(problemid);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product");
                return View("Error");
            }
        }

        //[Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<IActionResult> AddReply(int id)
        {
            var replies = await _problemService.GetRepliesForProblem(id);
            var st = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();
            ViewBag.St = new SelectList(st);
            return View(new Reply() { Id = id, Replies = replies.ToList() });

        }
        [HttpPost]
        //[Authorize(Roles= "Admin")]
        [Authorize]
        public async Task<IActionResult> AddReply(Reply model)
        {
            //model.Problems.Status = Status.Processing;
            var userIdBytes = HttpContext.Session.GetString("UserID");
            var userIdInt = Convert.ToInt32(userIdBytes);
            int currentUserId = userIdInt;
            await _problemService.AddReplyAsync(model.Id, model.ReplyDesc, model.Status, currentUserId);
            await _problemService.UpdateStatus(model.Id, model.Status);
            var st = Enum.GetValues(typeof(Status)).Cast<Status>().ToList();//from chatgpt
            ViewBag.st = new SelectList(st);
            return RedirectToAction("ProblemIndex", "Problem");
        }

        //[HttpGet]
        //public IActionResult ViewReplies(int id) 
        //{
        //    return View(new Reply() { Id = id });
        //}

        [Authorize]
        public async Task<IActionResult> ViewReplies(int id)
        {
            //var userIdBytes = HttpContext.Session.GetString("UserID");
            //var userIdInt = Convert.ToInt32(userIdBytes);
            //int currentUserId = userIdInt; 
            var replies = await _problemService.GetRepliesForProblem(id);
            ViewBag.problemId = id;
            return View(replies);
        }


        //*******************MyPart
        //[HttpGet]
        //public async Task<IActionResult> UserChangeStatus(int Id)
        //{
        //    return View();

        //}

        //[HttpPost]
        //public async Task<IActionResult> UserChangeStatus(int Id, Status newStatus)
        //{
        //    var userIdBytes = HttpContext.Session.GetString("UserID");
        //    var userIdInt = Convert.ToInt32(userIdBytes);
        //    int currentUserId = userIdInt;
        //    var problems = await _problemService.GetProblem(currentUserId);
        //    await _problemService.UpdateStatus(Id, newStatus);
        //    return View(problems);
        //}

        //*********************ChatGPT
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserChangeStatus()
        {
            var userIdBytes = HttpContext.Session.GetString("UserID");
            var userIdInt = Convert.ToInt32(userIdBytes);
            int currentUserId = userIdInt;
            // Assuming _problemService.GetProblem returns a single ProblemViewModel
            var problem = await _problemService.GetProblem(currentUserId);

            if (problem == null)
            {
                // Problem not found or not owned by the user
                return RedirectToAction("MyProblems", "Problem");
            }

            // Create a list of status options for the dropdown
            var statusOptions = Enum.GetValues(typeof(Status))
                                    .Cast<Status>()
                                    .Select(s => new SelectListItem
                                    {
                                        Value = ((int)s).ToString(),
                                        Text = s.ToString()
                                    })
                                    .ToList();

            ViewBag.StatusOptions = statusOptions;

            return View(problem);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserChangeStatus(int problemId, Status newStatus, Reply reply)
        {
            var userIdBytes = HttpContext.Session.GetString("UserID");
            var userIdInt = Convert.ToInt32(userIdBytes);
            int currentUserId = userIdInt;

            // Assuming _problemService.GetProblem returns a single ProblemViewModel
            await _problemService.AddReplyAsync(reply.ProblemId, reply.ReplyDesc, reply.Status, currentUserId);
            var problem = await _problemService.GetProblemByProblemId(problemId);

            if (problem != null)
            {
                await _problemService.UpdateStatus(problemId, newStatus);
            }

            return RedirectToAction("MyProblems", "Problem");
        }


    }
}
