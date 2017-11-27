using FShop.Data.Infrastructure;
using FShop.Entities.Models;
using System.Collections.Generic;
using System.Linq;

namespace FShop.Data.Repositories.Products
{
    //định nghĩa các phương thức cần thêm khi k có phương thức trong RepositoryBase
    public interface IProductCategoryRepository : IRepository<ProductCategory>
    {
        IEnumerable<ProductCategory> GetByAlias(string alias);
    }

    //thực thi các phương thức trên
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }

        public IEnumerable<ProductCategory> GetByAlias(string alias)
        {
            return this.DbContext.ProductCategories.Where(x => x.Alias == alias);
        }
    }
}