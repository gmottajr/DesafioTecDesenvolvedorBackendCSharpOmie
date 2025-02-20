﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Omie.Common.Abstractions.Application.Models;

namespace Omie.Common.Abstractions.Application.Services;

public interface IAppServiceBase<TDto, TDtoInserting, TKey> where TDto : IResourceDtoBase where TDtoInserting : IResourceDtoBase 
{
    Task<TDto> GetByIdAsync(TKey id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> AddAsync(TDtoInserting dto);
    Task<TDto> UpdateAsync(TDto dto);
    Task DeleteAsync(TKey id);
}
