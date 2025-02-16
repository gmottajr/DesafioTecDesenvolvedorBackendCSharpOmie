using Omie.DAL;
using Omie.Domain;
using Omie.Application.Models;
using MapsterMapper;

namespace Omie.Application.Services;

public class ClienteAppService : AppServiceBase<ClienteDto, ClienteInsertingDto, Cliente, long>, IClienteAppService
{
    public ClienteAppService(IClienteRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
