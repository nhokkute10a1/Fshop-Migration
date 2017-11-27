using FShop.Data.Infrastructure;
using FShop.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FShop.Data.Repositories.Slides
{
    public interface ISlideRepository : IRepository<Slide>
    {

    }

    public class SlideRepository : RepositoryBase<Slide>, ISlideRepository
    {
        public SlideRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

    }
}
