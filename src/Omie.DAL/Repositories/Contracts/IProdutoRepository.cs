using Omie.DAL.Abstractions;
using Omie.Domain;
using Omie.Domain.Entities;

namespace Omie.DAL;

public interface IProdutoRepository : IDataRepositoryBase<Produto, long>
{

}