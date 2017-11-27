using FShop.Data.Infrastructure;
using FShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data.Repositories.Menus
{
    public interface IMenuGroupRepository : IRepository<MenuGroup>
    {

    }

    public class MenuGroupRepository : RepositoryBase<MenuGroup>, IMenuGroupRepository
    {
        public MenuGroupRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

    }
}
