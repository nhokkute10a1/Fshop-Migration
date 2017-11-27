using FShop.Data.Infrastructure;
using FShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data.Repositories.VisitorStatistics
{
    public interface IVisitorStatisticRepository : IRepository<VisitorStatistic>
    {

    }

    public class VisitorStatisticRepository : RepositoryBase<VisitorStatistic>, IVisitorStatisticRepository
    {
        public VisitorStatisticRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

    }
}
