﻿using Omie.DAL;
using Omie.Application.Models;
using Omie.Domain;
using MapsterMapper;
using Omie.Domain.Entities;

namespace Omie.Application.Services;

public class ProdutoAppService : AppServiceBase<ProdutoDto, ProdutoInsertingDto, Produto, long>, IProdutoAppService
{
    public ProdutoAppService(IProdutoRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
