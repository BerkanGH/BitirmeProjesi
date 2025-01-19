using Microsoft.EntityFrameworkCore.Storage;
using OnlineShoppingPlatform.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        //Transaction durumlarını burada yapıyoruz.

        private readonly OnlineShoppingPlatformDbContext _db;
        private IDbContextTransaction _transaction;

        public UnitOfWork(OnlineShoppingPlatformDbContext db)
        {
            _db = db;
        }
        public async Task BeginTransaction()
        {
            _transaction = await _db.Database.BeginTransactionAsync();
        }

        public async Task CommitTransaction()
        {
           await _transaction.CommitAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task RollBackTransaction()
        {
            await _transaction.RollbackAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
