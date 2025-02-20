using Omie.DAL;
using Omie.Application.Models;
using MapsterMapper;
using Omie.Common.Abstractions.Application.Services;
using Omie.Domain.Entities;

namespace Omie.Application.Services;

public class ClienteAppService : AppServiceBase<ClienteDto, ClienteInsertingDto, Cliente, long>, IClienteAppService
{
    public ClienteAppService(IClienteRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
