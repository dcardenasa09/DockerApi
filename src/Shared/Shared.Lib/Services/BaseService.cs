using AutoMapper;
using Shared.Lib.Interfaces;
using System.Linq.Expressions;
using Shared.Lib.Repositories;

namespace Shared.Lib.Services;

public class BaseService<TEntity, TDto, TRepository> : IBaseService<TEntity, TDto> where TEntity : IEntity where TDto : class where TRepository : IBaseRepository<TEntity> {

    private readonly IMapper _mapper;
    private readonly TRepository _repository;

    public BaseService(IMapper mapper, TRepository repository) {
        _mapper     = mapper;
        _repository = repository;
    }

    public virtual async Task<TDto> GetFirst(Expression<Func<TEntity, bool>> predicate, string[]? includes = null, bool applyAsNoTracking = true) {
        predicate ??= (x => true);
        var resultEntity = await _repository.GetFirst(predicate, includes, applyAsNoTracking) ?? throw new KeyNotFoundException("data_not_found");
        var vm = _mapper.Map<TDto>(resultEntity);

        return vm;
    }

    public virtual async Task<List<TDto>> GetList(Expression<Func<TEntity, bool>> predicate, string[]? includes = null, bool applyAsNoTracking = true) {
        var resultEntity = await _repository.GetList(predicate, includes, applyAsNoTracking) ?? throw new KeyNotFoundException("data_not_found");
        var vm = _mapper.Map<List<TDto>>(resultEntity);

        return vm;
    }

    public virtual async Task<TDto> GetById(int id, string[]? includes = null, bool applyAsNoTracking = true) {
        var entity = await _repository.GetById(id, includes, applyAsNoTracking) ?? throw new KeyNotFoundException("data_not_found");
        return _mapper.Map<TDto>(entity);
    }

    public virtual async Task<TDto> Create(TDto dto) {
        var entity = _mapper.Map<TEntity>(dto);
        var savedEntity = await _repository.Create(entity);

        return _mapper.Map<TDto>(savedEntity);
    }

    public virtual async Task<TDto> Update(TDto dto) {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.Update(entity);

        return dto;
    }

    public virtual async Task Delete(int id) {
        await _repository.Delete(id);
    }
}