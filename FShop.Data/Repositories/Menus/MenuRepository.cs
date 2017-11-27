using FShop.Data.Infrastructure;
using FShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data.Repositories.Menus
{
    public interface IMenuRepository : IRepository<Menu>
    {

    }

    public class MenuRepository : RepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

    }
}
