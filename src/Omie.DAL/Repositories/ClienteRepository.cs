using Omie.DAL.Abstractions;
using Omie.Domain;
using Microsoft.EntityFrameworkCore;

namespace Omie.DAL;

public class ClienteRepository : DataRepositoryBase<Cliente, long>, IClienteRepository
{
    public ClienteRepository(DbContextOmie context) : base(context)
    {
    }
}
