using System;
using System.Collections.Generic;
using System.Text;

using LaBancaUCB.Core.Entities;
namespace LaBancaUCB.Core.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(long id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(long id);
}
