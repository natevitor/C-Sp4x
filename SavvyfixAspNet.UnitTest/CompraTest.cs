using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using SavvyfixAspNet.Domain.Entities;
using Xunit;

namespace SavvyfixAspNet.UnitTest;

public class CompraTest : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CompraTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetCompras_RetornaListaDeCompras()
    {
        // Act
        var response = await _client.GetAsync("/compras");

        // Assert
        response.EnsureSuccessStatusCode();
        var compras = await response.Content.ReadFromJsonAsync<IEnumerable<Compra>>();
    
        Assert.NotNull(compras);
        Assert.NotEmpty(compras);
    }

    [Fact]
    public async Task GetCompraById_RetornaCompra_SeCompraExiste()
    {
        // Arrange
        int compraId = 1;

        // Act
        var response = await _client.GetAsync($"/compras/{compraId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var compra = await response.Content.ReadFromJsonAsync<Compra>();

        Assert.NotNull(compra);
    }
    
    [Fact]
    public async Task GetCompraById_RetornaStatus404_SeCompraNaoExiste()
    {
        // Arrange
        int compraId = 999; 

        // Act
        var response = await _client.GetAsync($"/compras/{compraId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    
    [Fact]
    public async Task CriaCompra_RetornaCompraCadastrada()
    {
        // Arrange
        var novaCompra = new Compra()
        {
            NmProd = "Tenis Nike",
            EspcificacoesProd = "Tenis esportivo para corrida",
            QntdProd = 2,
            IdProd = 1,
            IdCliente = 1,
            IdAtividades = 1
        };

        // Act
        var response = await _client.PostAsJsonAsync("/compras", novaCompra);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var compraCadastrada = await response.Content.ReadFromJsonAsync<Compra>();
        Assert.Equal(novaCompra.NmProd, compraCadastrada.NmProd);
        Assert.Equal(novaCompra.EspcificacoesProd, compraCadastrada.EspcificacoesProd);
        Assert.Equal(novaCompra.QntdProd, compraCadastrada.QntdProd);
        Assert.Equal(466, compraCadastrada.ValorCompra);
        Assert.Equal(novaCompra.IdProd, compraCadastrada.IdProd);
        Assert.Equal(novaCompra.IdCliente, compraCadastrada.IdCliente);
        Assert.Equal(novaCompra.IdAtividades, compraCadastrada.IdAtividades);
    }

    
    [Theory]
    [InlineData(null, "Tenis para corrida", 2, 1, 1, 1)] // Nome do produto nulo
    [InlineData("", "Tenis para corrida", 2, 1, 1, 1)]   // Nome do produto vazio
    [InlineData("Tenis Nike", "", 2, 1, 1, 1)]           // Especificação vazia
    [InlineData("Tenis Nike", "Tenis para corrida", 0, 1, 1, 1)]  // Quantidade zero
    [InlineData("Tenis Nike", "Tenis para corrida", 0, 999, 1, 1)]  // Produto inexistente
    [InlineData("Tenis Nike", "Tenis para corrida", 0, 1, 999, 1)]  // Atividades inexistente
    [InlineData("Tenis Nike", "Tenis para corrida", 0, 1, 1, 999)]  // Cliente inexistente
    public async Task CriaCompra_RetornaErro_SeVariosAtributosInvalidos(string nomeProduto, string especificacoes, int quantidade, long idProduto, long idAtividades, long idCliente )
    {
        // Arrange
        var novaCompra = new Compra()
        {
            NmProd = nomeProduto,
            EspcificacoesProd = especificacoes,
            QntdProd = quantidade,
            IdProd = idProduto,        
            IdCliente = idCliente,     
            IdAtividades = idAtividades   
        };
    
        // Act
        var response = await _client.PostAsJsonAsync("/compras", novaCompra);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); 
    }

    
    [Fact]
    public async Task AtualizaCompra_RetornaCompraAtualizada_SeCompraExiste()
    {
        // Arrange
        int compraId = 1; 
        var atualizarCompra = new Compra()
        {
            IdCompra = compraId,
            NmProd = "Tenis Nike",
            EspcificacoesProd = "Tenis Casual",
            QntdProd = 2,
            IdProd = 1, 
            IdCliente = 1, 
            IdAtividades = 1 
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/compras/{compraId}", atualizarCompra);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var compraRetornada = await response.Content.ReadFromJsonAsync<Compra>();
        Assert.Equal(atualizarCompra.NmProd, compraRetornada.NmProd);
        Assert.Equal(atualizarCompra.EspcificacoesProd, compraRetornada.EspcificacoesProd);
        Assert.Equal(atualizarCompra.QntdProd, compraRetornada.QntdProd);
        Assert.Equal(466, compraRetornada.ValorCompra);
        Assert.Equal(atualizarCompra.IdProd, compraRetornada.IdProd);
        Assert.Equal(atualizarCompra.IdCliente, compraRetornada.IdCliente);
        Assert.Equal(atualizarCompra.IdAtividades, compraRetornada.IdAtividades);
    }


    [Fact]
    public async Task AtualizaCompra_RetornaCompraStatus404_SeCompraNaoExiste()
    {
        // Arrange
        int compraId = 999; 
        var atualizaCompra = new Compra()
        {
            IdCompra = compraId,
            NmProd = "Tenis Nike",
            EspcificacoesProd = "Tenis Casual",
            QntdProd = 2,
            IdProd = 1, 
            IdCliente = 1, 
            IdAtividades = 1 
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/compras/{compraId}", atualizaCompra);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    
    [Fact]
    public async Task DeletaCompra_RetornaNoContent_SeCompraExiste()
    {
        // Arrange
        int compraId = 2; 

        // Act
        var response = await _client.DeleteAsync($"/compras/{compraId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeletaCompra_RetornaStatus404_SeCompraNaoExiste()
    {
        // Arrange
        int compraId = 999; 

        // Act
        var response = await _client.DeleteAsync($"/compras/{compraId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

}