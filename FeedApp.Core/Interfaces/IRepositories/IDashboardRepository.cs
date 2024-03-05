﻿using FeedApp.Core.Entities.Business;
using FeedApp.Core.Entities.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Interfaces.IRepositories
{
    public interface IDashboardRepository
    {
        Task<List<Problems>> AdminDashboard();
        Task<List<Problems>> UserDashboard(int userid);
       
    }
}
