using Omie.DAL.Abstractions;
using Omie.Domain.Abstractions;
using Mapster;
using MapsterMapper;
using Omie.Application.Models.Abstractions;

namespace Omie.Application;

public class AppServiceBase<TDto, TEntity, TKey> : IAppServiceBase<TDto, TKey> where TDto : ResourceDtoBaseRoot<TKey> where TEntity : EntityBaseRoot<TKey>
{
    protected readonly IDataRepositoryBase<TEntity, TKey> _repository;
    public AppServiceBase(IDataRepositoryBase<TEntity, TKey> repository)
    {
        _repository = repository;
    }

    public virtual async Task<TDto> GetByIdAsync(TKey id)
    {
        var domain = await _repository.GetByIdAsync(id);
        return domain.Adapt<TDto>();
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var domains = await _repository.GetAllAsync();
        return domains.Adapt<IEnumerable<TDto>>();
    }

    public virtual async Task<TDto> AddAsync(TDto dto)
    {
        var domain = dto.Adapt<TEntity>();
        await _repository.AddAsync(domain);
        return domain.Adapt<TDto>();
    }

    public virtual async Task<TDto> UpdateAsync(TDto dto)
    {
        var domain = dto.Adapt<TEntity>();
        _repository.Update(domain);
        var updated = await _repository.GetByIdAsync(domain.Id);
        return updated.Adapt<TDto>();
    }   

    public virtual async Task DeleteAsync(TKey id)
    {
        await _repository.DeleteAsync(id);
    }
}