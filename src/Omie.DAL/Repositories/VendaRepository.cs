using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Omie.Common.Abstractions.DAL.Reposotories;
using Omie.Domain.Entities;

namespace Omie.DAL;

public class VendaRepository : DataRepositoryBase<Venda, long>, IVendaRepository
{
    public VendaRepository(DbContextOmie context) : base(context)
    {
    }

    public override async Task<Venda?> GetByIdAsync(long id)
    {
        var venda = (await _context.Set<Venda>()
            .AsNoTracking()
            .Include(v => v.Itens)
            .FirstOrDefaultAsync(v => v.Id == id));
        return venda;
    }

    public override async Task<IEnumerable<Venda>> GetAllAsync()
    {
        var vendas = await _context.Set<Venda>()
            .AsNoTracking()
            .Include(v => v.Itens)
            .ToListAsync();
        return vendas;
    }

    public override async Task<IEnumerable<Venda>> GetAllAsync(Expression<Func<Venda, bool>>? filter = null)
    {
        IQueryable<Venda> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        return await query.Include(v => v.Itens).ToListAsync();
    }
}
