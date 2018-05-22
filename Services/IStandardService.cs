using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Beehouse.FrameworkStandard.Entities;

namespace Beehouse.FrameworkStandard.Services
{
    public interface IStandardService<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> GetEntities(bool tracking = false);
        Task<TEntity> GetByIdAsync(string id, bool tracking = false, IQueryable<TEntity> query = null);
        Task<ICollection<TEntity>> GetAsync(bool tracking = false, IQueryable<TEntity> query = null);
        Task<SearchResult<TEntity>> GetAsync(int page, int limit, bool tracking = false, IQueryable<TEntity> query = null);
        Task<TEntity> InsertAsync(TEntity e);
        Task<TEntity> InsertOrUpdateAsync(TEntity m);
        Task<TEntity> UpdateAsync(TEntity entity, TEntity old = null);
    }
}