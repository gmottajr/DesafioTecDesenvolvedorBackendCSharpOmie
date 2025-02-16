using Omie.Application.Models;
using Omie.Domain.Abstractions;
using Omie.DAL.Abstractions;
using Omie.Application.Models.Abstractions;

namespace Omie.Application;

public interface IAppServiceBase<TDto, TDtoInserting, TKey> where TDto : IResourceDtoBase where TDtoInserting : IResourceDtoBase 
{
    Task<TDto> GetByIdAsync(TKey id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> AddAsync(TDtoInserting dto);
    Task<TDto> UpdateAsync(TDto dto);
    Task DeleteAsync(TKey id);
}
