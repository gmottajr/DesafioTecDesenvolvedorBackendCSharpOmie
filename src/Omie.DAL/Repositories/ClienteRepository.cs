using Omie.Common.Abstractions.DAL.Reposotories;
using Omie.Domain.Entities;

namespace Omie.DAL;

public class ClienteRepository : DataRepositoryBase<Cliente, long>, IClienteRepository
{
    public ClienteRepository(DbContextOmie context) : base(context)
    {
    }
}
