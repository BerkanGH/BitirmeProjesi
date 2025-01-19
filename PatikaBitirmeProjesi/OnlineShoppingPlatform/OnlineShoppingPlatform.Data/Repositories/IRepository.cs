using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShoppingPlatform.Data.Repositories
{
    public interface IRepository<TEntity>
        where TEntity : class
    {

        //projede yapacağım genel işlemleri tanımlıyorum.
        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity, bool softDelete = true);

        void Delete(int id);

        TEntity GetById(int id);

        TEntity FindById(int id);

        TEntity Get(Expression<Func<TEntity, bool>> predicate);

        // bu şekilde x.name gibi aramaları yapabiliyoruz predicate te parametre

        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null);
    }
}
