using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        //Transaction durumları için oluşturdum.

        Task<int> SaveChangesAsync();
        // kaç kayda etki ettiğini gösterdiği için int

        Task BeginTransaction();

        Task CommitTransaction();

        Task RollBackTransaction();
    }
}
