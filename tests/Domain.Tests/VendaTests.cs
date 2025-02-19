using AutoFixture;
using FakeItEasy;
using FluentAssertions;
using MapsterMapper;
using Omie.Application;
using Omie.Application.Models;
using Omie.Application.Services;
using Omie.DAL;
using Omie.Domain.Entities;

namespace Domain.Tests;

public class VendaTests
{
    private readonly IVendaAppService _vendaAppService;
    private readonly IVendaRepository _fakeVendaRepository;
    private readonly IMapper _fakeMapper;

    public VendaTests()
    {
        _fakeVendaRepository = A.Fake<IVendaRepository>();
        _fakeMapper = A.Fake<IMapper>();
        _vendaAppService = new VendaAppService(_fakeVendaRepository, _fakeMapper);
    }
    [Fact]
    public void Sale_Code_must_be_generated_automatically()
    {
        var sale = new Venda
        {
            Id = 1,
            CodigoVenda = _vendaAppService.GenerateCodigoVenda(new VendaInsertingDto() {Cliente = new Fixture().Create<string>()}),
        };
        sale.CodigoVenda.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void Sale_Items_should_be_initialized()
    {
        var sale = new Venda
        {
            Id = 1
        };
        sale.Itens.Should().NotBeNull();
    }
}