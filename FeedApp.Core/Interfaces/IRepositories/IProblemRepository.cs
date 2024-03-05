using FeedApp.Core.Entities.Business;
using FeedApp.Core.Entities.General;
using FeedApp.Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedApp.Core.Interfaces.IRepositories
{
    public interface IProblemRepository : IBaseRepository<Problems>
    {
        Task<PaginatedDataViewModel<Problems>> GetPaginatedData(int pageNumber, int pageSize);
    }
}
