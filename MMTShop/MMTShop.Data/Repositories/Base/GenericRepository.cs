using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MMTShop.Data.Entities.Base;

namespace MMTShop.Data.Repositories.Base
{
    /// <summary>
    /// Generic Repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity
    {
        protected readonly DataContext context;
        private DbSet<TEntity> entities;

        public GenericRepository(DataContext context)
        {
            this.context = context;
        }

        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (entities == null)
                    entities = context.Set<TEntity>();
                return entities;
            }
        }

        public virtual TEntity Get(int id)
        {
            return Entities.Find(id);
        }

        public virtual async Task<TEntity> GetAsync(int id)
        {
            return await Entities.FindAsync(id);
        }
        /// <summary>
        /// Get all entities from database
        /// </summary>
        /// <param name="page">zero based page index</param>
        /// <param name="size">size of page</param>
        /// <returns>IEnumerable<TEntity></returns>
        public virtual IEnumerable<TEntity> GetAll(int page, int size, bool onlyActive)
        {
            if (onlyActive)
                return Entities.Where(a => a.IsActive == onlyActive).Skip((page - 1) * size).Take(size).ToList();
            return Entities.Skip((page - 1) * size).Take(size).ToList();
        }
        /// <summary>
        /// Get all entities from database async
        /// </summary>
        /// <param name="page">zero based page index</param>
        /// <param name="size">size of page</param>
        /// <returns>IEnumerable<TEntity></returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(int page, int size, bool onlyActive)
        {
            if (onlyActive)
                return await Entities.Where(a => a.IsActive == onlyActive).Skip((page - 1) * size).Take(size).ToListAsync();
            return await Entities.Skip((page - 1) * size).Take(size).ToListAsync();
        }

        public virtual void Add(TEntity entity)
        {
            Entities.Add(entity);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await Entities.AddAsync(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await Entities.AddRangeAsync(entities);
        }

        public virtual void Remove(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            Entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            Entities.UpdateRange(entities);
        }

        public int Count(Func<TEntity, bool> filterExpression = null,string[] Includes=null)
        {
            var currentEntities = Entities.AsQueryable();
            if(Includes!=null && Includes.Length > 0)
            {
                foreach (var item in Includes)
                {
                    currentEntities = currentEntities.Include(item);
                }
            }
            if (filterExpression != null)
                return currentEntities.Where(filterExpression).Count();
            return currentEntities.Count();
        }
    }
}
