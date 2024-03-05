using FeedApp.Core.Entities.General;
using FeedApp.Core.Entities.Business;
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
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public DashboardRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Problems>> AdminDashboard()
        {
            return await _dbContext.Problems.ToListAsync();
        }

        public async Task<List<Problems>> UserDashboard(int userid)
        {
            return await _dbContext.Problems.Where(x => x.UserId == userid).ToListAsync();
        }

        
        
    }
}
