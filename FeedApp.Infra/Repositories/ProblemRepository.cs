using FeedApp.Core.Entities.General;
using FeedApp.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeedApp.Infra.Repositories;
using FeedApp.Infra.Data;
using Microsoft.EntityFrameworkCore;
using FeedApp.Core.Entities.Business;

namespace FeedApp.Infra.Repositories
{
    public class ProblemRepository : BaseRepository<Problems>, IProblemRepository
    {
        public ProblemRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<PaginatedDataViewModel<Problems>> GetPaginatedData(int pageNumber, int pageSize)
        {
            var query = _dbContext.Problems//Shob data eshe portese
                .Include(x=>x.Users)
                .OrderByDescending(r => r.EntryDate)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking();

            var data = await query.ToListAsync();
            var totalCount = await _dbContext.Problems.CountAsync();/* CountAsync(item => item.UserID == userId);*/

            return new PaginatedDataViewModel<Problems>(data, totalCount);

        }

    }
}
