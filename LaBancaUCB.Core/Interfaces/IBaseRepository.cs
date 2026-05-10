using System.Collections.Generic;
using System.Threading.Tasks;
using LaBancaUCB.Core.Entities;

namespace LaBancaUCB.Core.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        void Update(T entity);

        Task DeleteAsync(long id);
    }
}