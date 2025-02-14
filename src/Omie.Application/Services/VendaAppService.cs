using Omie.Application.Models;
using Omie.DAL;
using Omie.Domain;


namespace Omie.Application.Services;

public class VendaAppService : AppServiceBase<VendaDto, Venda, long>, IVendaAppService
{
    public VendaAppService(IVendaRepository repository) : base(repository)
    {
    }
}
