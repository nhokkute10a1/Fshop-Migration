using System.Collections.Generic;
using System.Linq;

namespace LibCommon
{
    public class DataGrid<T>
    {
        public int TotalCounts { get; set; }
        public double TotalPages { get; set; }
        public ICollection<T> Records { get; set; }
    }

    public sealed class PagingDataGrid<TEntity> where TEntity : class
    {
        public List<TEntity> PageManyAsQueryable(int PageNumber, int PageSize, IQueryable<TEntity> Query, out int TotalCounts, out double TotalPages)
        {
            TotalCounts = Query.Count();
            TotalPages = Query.Count() / PageSize == 0 ? 1 : Query.Count() / PageSize;
            return Query.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}