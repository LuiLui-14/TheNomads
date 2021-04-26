using System.Linq;
using System.Threading.Tasks;

namespace Playlistofy.Data.Abstract
{
    /// <summary>
    /// Minimal interface for CRUD operations on entities
    /// </summary>
    /// <typeparam name="TEntity">This is the entity for which we're making a repository</typeparam>
    public interface IRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> FindByIdAsync(string id);
        Task<bool> ExistsAsync(string id);

        IQueryable<TEntity> GetAll();

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        Task DeleteAsync(TEntity entity);
        Task DeleteByIdAsync(string id);
    }
}