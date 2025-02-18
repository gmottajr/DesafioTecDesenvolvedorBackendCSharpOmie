﻿using MapsterMapper;
using Microsoft.VisualBasic.CompilerServices;
using Omie.Application.Models;
using Omie.DAL;
using Omie.Domain;
using Omie.Domain.Entities;

namespace Omie.Application.Services;

public class VendaAppService : AppServiceBase<VendaDto, VendaInsertingDto, Venda, long>, IVendaAppService
{
    public VendaAppService(IVendaRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }

    /// <summary>
    /// Regra hipotética para geração do código da venda
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public string GeraCodigoVenda(VendaInsertingDto dto)
    {
        if (dto == null)
            throw new InvalidOperationException("Não foi possível gerar o código da venda: Venda inexistente.");
        if (dto.ClienteId <= 0)
            throw new ArgumentException("Não foi possível gerar o código da venda: Cliente inexistente.");
        
        return $"{DateTime.UtcNow:yyyyMMddHHmmss}-{dto.ClienteId}";
    }

    public async Task<bool> Cancelar(long id)
    {
        try
        {
            var venda = await _repository.GetByIdAsync(id);
            venda.CancelledAt = DateTime.Now;
            _repository.Update(venda);
            await _repository.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> CompletarVenda(long id, DateTime dataCompletado)
    {
        try
        {
            var venda = await _repository.GetByIdAsync(id);
            venda.CompletedAt = dataCompletado;
            _repository.Update(venda);
            await _repository.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task<bool> AlterarData(long id, DateTime dataAlterado)
    {
        try
        {
            var venda = await _repository.GetByIdAsync(id);
            venda.DataDaVenda = dataAlterado;
            _repository.Update(venda);
            await _repository.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public override async Task DeleteAsync(long id)
    {
        try
        {
            var venda = await _repository.GetByIdAsync(id);
            venda.DeletedAt = DateTime.Now;
            _repository.Update(venda);
            await _repository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public override async Task<IEnumerable<VendaDto>> GetAllAsync()
    {
        var domains = await _repository.GetAllAsync(v => v.DeletedAt == null);
        return _mapper.Map<IEnumerable<VendaDto>>(domains);
    }
}
