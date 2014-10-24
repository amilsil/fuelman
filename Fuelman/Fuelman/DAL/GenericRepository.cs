using Fuelman.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace Fuelman.DAL
{
    public class GenericRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IBaseEntity
    {
        internal VehicleDbContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(VehicleDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id, string includeProperties = "")
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            // Create an entry in the context for the entityToUpdate
            // If it is Detached, we check if there's an entry matching the same Id.
            // If we do, we update that entry with the new values.
            // Else, we can set the new entry modified. 
            // which will save the entry to the database.

            var entry = context.Entry<TEntity>(entityToUpdate);
            if (entry.State == EntityState.Detached)
            {
                TEntity attachedEntity = dbSet.SingleOrDefault(e => e.Id == entityToUpdate.Id);
                if (attachedEntity != null)
                {
                    var attachedEntry = context.Entry(attachedEntity);
                    attachedEntry.CurrentValues.SetValues(entityToUpdate);
                }
                else
                {
                    entry.State = EntityState.Modified;
                }
            }
            
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }
    }
}