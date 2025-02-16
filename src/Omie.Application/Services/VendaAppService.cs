using MapsterMapper;
using Omie.Application.Models;
using Omie.DAL;
using Omie.Domain;

namespace Omie.Application.Services;

public class VendaAppService : AppServiceBase<VendaDto, VendaInsertingDto, Venda, long>, IVendaAppService
{
    public VendaAppService(IVendaRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
