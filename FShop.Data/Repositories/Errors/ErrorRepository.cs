using FShop.Data.Infrastructure;
using FShop.Entities.Models;

namespace FShop.Data.Repositories.Errors
{
    public interface IErrorRepository : IRepository<Error>
    {
    }

    public class ErrorRepository : RepositoryBase<Error>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}