using Omie.DAL;
using Omie.Domain;
using Omie.Application.Models;

namespace Omie.Application.Services;

public class ClienteAppService : AppServiceBase<ClienteDto, Cliente, long>, IClienteAppService
{
    public ClienteAppService(IClienteRepository repository) : base(repository)
    {
    }
}
