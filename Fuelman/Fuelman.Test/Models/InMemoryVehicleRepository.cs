using Fuelman.DAL;
using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fuelman.Test.Models
{
    class InMemoryVehicleRepository<TEntity> : IVehicleRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        private List<TEntity> _db = new List<TEntity>();

        public Exception ExceptionToThrow { get; set; }

        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            return _db.ToList();
        }

        public TEntity GetByID(object id)
        {
            return _db.FirstOrDefault(d => d.Id == (int) id);
        }

        public void Insert(TEntity entity)
        {
            _db.Add(entity);
        }

        public void Delete(object id)
        {
            _db.Remove(GetByID(id));
        }

        public void Update(TEntity entity)
        {
            // Nothing to do. :)
        }

        public void Save()
        {
            // Nothing to do :)
        }


        public void Delete(TEntity entityToDelete)
        {
            _db.Remove(entityToDelete);
        }
    }
}
