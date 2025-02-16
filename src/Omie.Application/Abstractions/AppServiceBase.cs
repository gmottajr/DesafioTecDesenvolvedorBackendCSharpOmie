using Omie.DAL.Abstractions;
using Omie.Domain.Abstractions;
using Mapster;
using MapsterMapper;
using Omie.Application.Models.Abstractions;

namespace Omie.Application;

public class AppServiceBase<TDto, TDtoInserting, TEntity, TKey> : IAppServiceBase<TDto, TDtoInserting, TKey> where TDto : ResourceDtoBaseRoot<TKey> where TEntity : EntityBaseRoot<TKey> where TDtoInserting : IResourceDtoBase
{
    protected readonly IDataRepositoryBase<TEntity, TKey> _repository;
    protected readonly IMapper _mapper;
    public AppServiceBase(IDataRepositoryBase<TEntity, TKey> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<TDto> GetByIdAsync(TKey id)
    {
        var domain = await _repository.GetByIdAsync(id);
        return _mapper.Map<TDto>(domain);
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        var domains = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<TDto>>(domains);
    }

    public virtual async Task<TDto> AddAsync(TDtoInserting dto)
    {
        var domain = _mapper.Map<TEntity>(dto);
        await _repository.AddAsync(domain);
        await _repository.SaveChangesAsync();
        return _mapper.Map<TDto>(domain);
    }

    public virtual async Task<TDto> UpdateAsync(TDto dto)
    {
        var domain = _mapper.Map<TEntity>(dto);
        _repository.Update(domain);
        var updated = await _repository.GetByIdAsync(domain.Id);
        return _mapper.Map<TDto>(updated);
    }   

    public virtual async Task DeleteAsync(TKey id)
    {
        await _repository.DeleteAsync(id);
    }
}
