using System.Text.Json;
using Shared.Lib.Models;
using Shared.Lib.Helpers;
using Shared.Lib.Interfaces;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Shared.Lib.Repositories;

public class BaseRepository<T>(DbContext context) : IBaseRepository<T> where T : class, IEntity {
    private readonly DbContext _context = context;

    public virtual async Task<T> Create(T entity) {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();

        return entity;
    }

    public virtual async Task<bool> CreateRange(List<T> entity) {
        bool response = false;

        await _context.Set<T>().AddRangeAsync(entity);
        var rowsCreated = await _context.SaveChangesAsync();

        if(rowsCreated > 0) {
            response = true;
        }

        return response;
    }

    public virtual async Task<List<T>> GetList(Expression<Func<T, bool>> predicate,
                                               string[]? includes = null,
                                               bool applyAsNoTracking = true) {

        var query = _context.Set<T>().AsQueryable();
        if (includes != null && includes.Length > 0) {
            foreach (var include in includes) {
                query = query.Include(include);
            }
        }

        query = query.Where(predicate);
        if (applyAsNoTracking) {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    public virtual async Task<T> GetFirst(Expression<Func<T, bool>> predicate,
                                          string[]? includes = null,
                                          bool applyAsNoTracking = true) {

        var query =  _context.Set<T>().AsQueryable();
        if (includes != null && includes.Length > 0) {
            foreach (var include in includes) {
                query = query.Include(include);
            }
        }

        query = query.Where(predicate);

        if (applyAsNoTracking) {
            var entity = await query.AsNoTracking().FirstOrDefaultAsync();
            _context.Entry(entity).State = EntityState.Detached;

            return entity;
        } else {
            return await query.FirstOrDefaultAsync();
        }
    }

    public virtual async Task<T> GetById(int id,
                                 string[]? includes = null,
                                 bool applyAsNoTracking = true) {

        var query = _context.Set<T>().AsQueryable();
        if (includes != null && includes.Length > 0) {
            foreach (var include in includes) {
                query = query.Include(include);
            }
        }

        if (applyAsNoTracking) {
            query = query.AsNoTracking();
        }

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task<bool> Update(T entity) {
        bool response = false;

        EntityEntry dbEntityEntry = _context.Entry(entity);
        if (dbEntityEntry.State == EntityState.Detached) {
            _context.Set<T>().Attach(entity);
        }

        dbEntityEntry.State = EntityState.Modified;
        int numEntriesChanged = await _context.SaveChangesAsync();
        if(numEntriesChanged > 0) {
            response = true;
        }

        return response;
    }

    public virtual async Task<bool> Delete(int id) {
        bool response = false;
        var entity = await _context.Set<T>().FindAsync(id) ?? throw new KeyNotFoundException("data_not_found");
        _context.Set<T>().Remove(entity);
        int numEntriesChanged = await _context.SaveChangesAsync();
        if(numEntriesChanged > 0) {
            response = true;
        }

        return response;
    }

    public virtual async Task SaveChangesAsync() {
        await _context.SaveChangesAsync();
    }
}