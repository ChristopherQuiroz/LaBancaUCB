using System;
using System.Collections.Generic;
using System.Text;
using LaBancaUCB.Core.Entities;
using LaBancaUCB.Core.Interfaces;
using LaBancaUCB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LaBancaUCB.Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly LaBancaUCBContext _context;
    private readonly DbSet<T> _entities;

    public BaseRepository(LaBancaUCBContext context)
    {
        _context = context;
        _entities = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _entities.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(long id)
    {
        return await _entities.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        _entities.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _entities.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var entity = await _entities.FindAsync(id);
        if (entity != null)
        {
            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
