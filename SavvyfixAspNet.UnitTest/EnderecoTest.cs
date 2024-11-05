using System.Collections;
using System.Net;
using System.Net.Http.Json;
using SavvyfixAspNet.Domain.Entities;
using Xunit;

namespace SavvyfixAspNet.UnitTest;

[Collection("Integration Tests")]
public class EnderecoTest  
{
    private readonly HttpClient _client;

    public EnderecoTest(CustomWebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
      [Fact]
    public async Task GetEnderecos_ReturnsListaEnderecos()
    {
        // Act
        var response = await _client.GetAsync("/enderecos");

        // Assert
        response.EnsureSuccessStatusCode();
        var enderecos = await response.Content.ReadFromJsonAsync<IEnumerable<Endereco>>();
        
        Assert.NotNull(enderecos);
        Assert.NotEmpty(enderecos);
    }
    
    [Fact]
    public async Task GetEnderecoById_RetornaEndereco_SeEnderecoExiste()
    {
        // Arrange
        int enderecoId = 1;

        // Act
        var response = await _client.GetAsync($"/enderecos/{enderecoId}");

        // Assert
        response.EnsureSuccessStatusCode();
        var endereco = await response.Content.ReadFromJsonAsync<Endereco>();

        Assert.NotNull(endereco);
    }
    
    [Fact]
    public async Task GetEnderecoById_RetornaStatus404_SeEnderecoNaoExiste()
    {
        // Arrange
        int enderecoId = 999; 

        // Act
        var response = await _client.GetAsync($"/enderecos/{enderecoId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task CriaEndereco_RetornaEnderecoCadastrado()
    {
        // Arrange
        var novoEndereco = new Endereco()
        {
            CepEndereco = "22260000", 
            RuaEndereco = "Rua das Laranjeiras",
            NumEndereco = "789",
            BairroEndeereco = "Laranjeiras",
            CidadeEndereco = "Rio de Janeiro",
            EstadoEndereco = "RJ",
            PaisEndereco = "Brasil"
        };

        // Act
        var response = await _client.PostAsJsonAsync("/enderecos", novoEndereco);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var enderecoCadastrado = await response.Content.ReadFromJsonAsync<Endereco>();
        Assert.Equal(novoEndereco.CepEndereco, enderecoCadastrado.CepEndereco);
        Assert.Equal(novoEndereco.RuaEndereco, enderecoCadastrado.RuaEndereco);
        Assert.Equal(novoEndereco.NumEndereco, enderecoCadastrado.NumEndereco);
        Assert.Equal(novoEndereco.BairroEndeereco, enderecoCadastrado.BairroEndeereco);
        Assert.Equal(novoEndereco.CidadeEndereco, enderecoCadastrado.CidadeEndereco);
        Assert.Equal(novoEndereco.EstadoEndereco, enderecoCadastrado.EstadoEndereco);
        Assert.Equal(novoEndereco.PaisEndereco, enderecoCadastrado.PaisEndereco);
        
    }
    
    [Theory]
    [InlineData(null, "Av Paulista", "123", "Paulista", "São Paulo", "SP", "Brasil")] // CEP nulo
    [InlineData("06414", "Av Paulista", "123", "Paulista", "São Paulo", "SP", "Brasil")] // CEP incorreto
    [InlineData("06414025", null, "123", "Paulista", "São Paulo", "SP", "Brasil")] // Rua nula
    [InlineData("06414025", "Av Paulista", "", "Paulista", "São Paulo", "SP", "Brasil")] // Número vazio
    [InlineData("06414025", "Av Paulista", "123", "", "São Paulo", "SP", "Brasil")] // Bairro vazio
    [InlineData("06414025", "Av Paulista", "123", "Paulista", "", "SP", "Brasil")] // Cidade vazia
    [InlineData("06414025", "Av Paulista", "123", "Paulista", "São Paulo", "", "Brasil")] // Estado vazio
    [InlineData("06414025", "Av Paulista", "123", "Paulista", "São Paulo", "SP", null)] // País nulo
    public async Task CriaEndereco_RetornaErro_SeVariosAtributosInvalidos(string cep, string rua, string numero, string bairro, string cidade, string estado, string pais)
    {
        // Arrange
        var novoEndereco = new Endereco()
        {
            CepEndereco = cep, 
            RuaEndereco = rua,
            NumEndereco = numero,
            BairroEndeereco = bairro,
            CidadeEndereco = cidade,
            EstadoEndereco = estado,
            PaisEndereco = pais
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/enderecos", novoEndereco);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode); 
        
    }
    
    [Fact]
    public async Task AtualizaEndereco_RetornaEnderecoAtualizado_SeEnderecoExiste()
    {
        // Arrange
        int enderecoId = 1;
        var atualizarEndereco = new Endereco()
        {
            IdEndereco = enderecoId,
            CepEndereco = "06414025",
            RuaEndereco = "Av Faria Lima",
            NumEndereco = "456",
            BairroEndeereco = "Jardim Paulistano",
            CidadeEndereco = "São Paulo",
            EstadoEndereco = "SP",
            PaisEndereco = "Brasil"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/enderecos/{enderecoId}", atualizarEndereco);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    
        var enderecoRetornado = await response.Content.ReadFromJsonAsync<Endereco>();
        Assert.Equal(atualizarEndereco.CepEndereco, enderecoRetornado.CepEndereco);
        Assert.Equal(atualizarEndereco.RuaEndereco, enderecoRetornado.RuaEndereco);
        Assert.Equal(atualizarEndereco.NumEndereco, enderecoRetornado.NumEndereco);
        Assert.Equal(atualizarEndereco.BairroEndeereco, enderecoRetornado.BairroEndeereco);
        Assert.Equal(atualizarEndereco.CidadeEndereco, enderecoRetornado.CidadeEndereco);
        Assert.Equal(atualizarEndereco.EstadoEndereco, enderecoRetornado.EstadoEndereco);
        Assert.Equal(atualizarEndereco.PaisEndereco, enderecoRetornado.PaisEndereco);
    }


    [Fact]
    public async Task AtualizaEndereco_RetornaEnderecoStatus404_SeEnderecoNaoExiste()
    {
        // Arrange
        int enderecoId = 999; // ID que não existe
        var atualizarEndereco = new Endereco()
        {
            IdEndereco = enderecoId,
            CepEndereco = "06414025",
            RuaEndereco = "Av Faria Lima",
            NumEndereco = "456",
            BairroEndeereco = "Jardim Paulistano",
            CidadeEndereco = "São Paulo",
            EstadoEndereco = "SP",
            PaisEndereco = "Brasil"
        };

        // Act
        var response = await _client.PutAsJsonAsync($"/enderecos/{enderecoId}", atualizarEndereco);

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    
    [Fact]
    public async Task DeletaEndereco_RetornaNoContent_SeEnderecoExiste()
    {
        // Arrange
        int enderecoId = 2; 

        // Act
        var response = await _client.DeleteAsync($"/enderecos/{enderecoId}");

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task DeletaEndereco_RetornaStatus404_SeEnderecoNaoExiste()
    {
        // Arrange
        int enderecoId = 999; 

        // Act
        var response = await _client.DeleteAsync($"/enderecos/{enderecoId}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

}