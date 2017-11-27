using System;

namespace FShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        FShopDbContext Init();
    }
}