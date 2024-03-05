using FeedApp.Core.Entities.General;
using FeedApp.Core.Interfaces.IRepositories;
using FeedApp.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Services
{
    public class DashboardService : IDashboardService
    {

        private readonly IDashboardRepository _dashboardRepository;



        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;

        }

        public async Task<List<Problems>> AdminDashboard()
    {
        
        return await _dashboardRepository.AdminDashboard();
    }

        public async Task<List<Problems>> UserDashboard(int userId)
        {
            
            return await _dashboardRepository.UserDashboard(userId);
        }
    }
}
