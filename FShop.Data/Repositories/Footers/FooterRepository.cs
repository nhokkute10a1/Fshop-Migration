using FShop.Data.Infrastructure;
using FShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data.Repositories.Footers
{
    public interface IFooterRepository : IRepository<Footer>
    {

    }

    public class FooterRepository : RepositoryBase<Footer>, IFooterRepository
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

    }
}
