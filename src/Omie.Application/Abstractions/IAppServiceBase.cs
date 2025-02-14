using Omie.Application.Models;
using Omie.Domain.Abstractions;
using Omie.DAL.Abstractions;
using Omie.Application.Models.Abstractions;

namespace Omie.Application;

public interface IAppServiceBase<TDto, TKey> where TDto : IResourceDtoBase 
{
    Task<TDto> GetByIdAsync(TKey id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> AddAsync(TDto dto);
    Task<TDto> UpdateAsync(TDto dto);
    Task DeleteAsync(TKey id);
}
