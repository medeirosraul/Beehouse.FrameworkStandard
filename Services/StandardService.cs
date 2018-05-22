using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Beehouse.FrameworkStandard.Entities;
using Microsoft.EntityFrameworkCore;

namespace Beehouse.FrameworkStandard.Services
{
    public class StandardService<TEntity> : IStandardService<TEntity> where TEntity:Entity
    {
        protected readonly DbContext Context;

        public StandardService(DbContext context)
        {
            Context = context;
            Debug.WriteLine($">>>> Instance of StandardService<{nameof(TEntity)}>");
        }

        private DbSet<TEntity> _entities;

        protected DbSet<TEntity> Entities => _entities ?? (_entities = Context.Set<TEntity>());

        public virtual IQueryable<TEntity> GetEntities(bool tracking = false)
        {
            var query = tracking ? Entities.AsTracking() : Entities.AsNoTracking();
            return query;
        }

        public virtual async Task<TEntity> GetByIdAsync(string id, bool tracking = false, IQueryable<TEntity> query = null)
        {
            if (query is null) query = tracking ? Entities.AsTracking() : Entities.AsNoTracking();
            return await query.FirstOrDefaultAsync(p => p.Id == id);
        }

        public virtual async Task<ICollection<TEntity>> GetAsync(bool tracking = false, IQueryable<TEntity> query = null)
        {
            if (query is null) query = tracking ? Entities.AsTracking() : Entities.AsNoTracking();
            return await query.ToListAsync();
        }

        public virtual async Task<SearchResult<TEntity>> GetAsync(int page, int limit, bool tracking = false, IQueryable<TEntity> query = null)
        {
            // Check param
            page = page <= 0 ? 1 : page;
            limit = limit <= 0 ? 10 : limit;

            // Check query
            if (query == null) query = tracking ? Entities.AsTracking() : Entities.AsNoTracking();

            // Query and count
            var count = await query.CountAsync();

            // Page and limit
            query = query.Skip((page - 1) * limit).Take(limit);

            // Result
            var result = new SearchResult<TEntity>
            {
                Page = page,
                Limit = limit,
                Count = count,
                Items = await query.ToListAsync()
            };

            return result;
        }

        public virtual async Task<TEntity> InsertAsync(TEntity e)
        {
            Debug.WriteLine($">>>> Saving on StandardService<{nameof(TEntity)}>.InsertAsync");
            e.CreatedAt = DateTime.Now;
            e.UpdatedAt = DateTime.Now;
            e.Deleted = false;

            await Entities.AddAsync(e);
            await Context.SaveChangesAsync();

            return e;
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity m)
        {
            if (!string.IsNullOrWhiteSpace(m.Id))
            {
                m.UpdatedAt = DateTime.Now;
                Entities.Attach(m);
                Context.Entry(m).State = EntityState.Modified;
            }
            else
            {
                m.CreatedAt = DateTime.Now;
                m.UpdatedAt = DateTime.Now;
                m.Deleted = false;

                await Entities.AddAsync(m);
            }

            await Context.SaveChangesAsync();

            return m;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entity, TEntity old = null)
        {
            // Compare with old entity
            if (old is null)
                if (!IsAttached(entity))
                    old = await (GetByIdAsync(entity.Id)) ??
                          throw new Exception("GenericService => Entity doesn't exists in database to update.");


            // Default values
            entity.UpdatedAt = DateTime.Now;

            // Attach

            Entities.Update(entity);

            // Resolve Columns
            ChangeColumnsState(ref entity);

            // Do update
            await Context.SaveChangesAsync();

            // Return result
            return entity;
        }

        protected virtual void ChangeColumnsState(ref TEntity entity)
        {
            // Exclude columns from update
            Context.Entry(entity).Property(p => p.CreatedAt).IsModified = false;
        }

        protected bool IsAttached(TEntity entity)
        {
            return Entities.Local.Any(e => e.Id == entity.Id);
        }

    }
}