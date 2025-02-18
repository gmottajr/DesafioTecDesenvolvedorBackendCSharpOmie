using Omie.DAL.Abstractions;
using Omie.Domain;
using Microsoft.EntityFrameworkCore;
using Omie.Domain.Entities;

namespace Omie.DAL;

public class VendaRepository : DataRepositoryBase<Venda, long>, IVendaRepository
{
    public VendaRepository(DbContextOmie context) : base(context)
    {
    }
}
