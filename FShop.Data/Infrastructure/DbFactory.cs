namespace FShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private FShopDbContext dbContext;

        public FShopDbContext Init()
        {
            return dbContext ?? (dbContext = new FShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}