using Shared.Lib.Models;
using System.Linq.Expressions;

namespace Shared.Lib.Repositories;

public interface IBaseRepository<T> {
    Task<T> Create(T entity);
    Task<bool> CreateRange(List<T> entity);
    Task<bool> Update(T entity);
    Task<bool> Delete(int id);
    Task<List<T>> GetList(Expression<Func<T, bool>> predicate, string[]? includes = null, bool applyAsNoTracking = true);
    Task<T> GetFirst(Expression<Func<T, bool>> predicate, string[]? includes = null, bool applyAsNoTracking = true);
    Task<T> GetById(int id, string[]? includes = null, bool applyAsNoTracking = true);
    Task SaveChangesAsync();
}