using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SavvyfixAspNet.Data;
using System.Linq;
using SavvyfixAspNet.Domain.Entities;

public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove a configuração existente do banco de dados
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<SavvyfixMetadataDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Configura o banco de dados InMemory para testes
            services.AddDbContext<SavvyfixMetadataDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDbForTesting");
            });

            // Popula o banco de dados InMemory
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<SavvyfixMetadataDbContext>();
            db.Database.EnsureCreated();
            SeedDatabase(db);
        });
    }

    private void SeedDatabase(SavvyfixMetadataDbContext dbContext)
    {
        if (!dbContext.Produtos.Any())
        {
            dbContext.Produtos.Add(new Produto { 
                IdProd = 1, 
                NmProd = "Tenis Adidas", 
                DescProd = "Tenis para corrida", 
                MarcaProd = "Adidas", 
                PrecoFixo = 450, 
                Img = "teste" });
            dbContext.Produtos.Add(new Produto
            {
                IdProd = 2,
                NmProd = "Tenis Nike",
                DescProd = "Tenis esportivo de alto performance",
                MarcaProd = "Nike",
                PrecoFixo = 750,
                Img = "tenis_nike_teste"
            });

            dbContext.Enderecos.Add(new Endereco()
            {
                IdEndereco = 1,
                CepEndereco = "06414025", 
                RuaEndereco = "Av Paulista",
                NumEndereco = "123",
                BairroEndeereco = "Paulista",
                CidadeEndereco = "São Paulo",
                EstadoEndereco = "SP",
                PaisEndereco = "Brasil"
            });
            dbContext.Enderecos.Add(new Endereco()
            {
                IdEndereco = 2,
                CepEndereco = "01001000", 
                RuaEndereco = "Rua da Consolação",
                NumEndereco = "456",
                BairroEndeereco = "Centro",
                CidadeEndereco = "São Paulo",
                EstadoEndereco = "SP",
                PaisEndereco = "Brasil"
            });
            dbContext.Clientes.Add(new Cliente()
            {
                IdCliente = 1,
                NmClie = "Luiz Teste",
                CpfClie = "83320946900",
                SenhaClie = "12345678",
                IdEndereco = 1
            });
            dbContext.Clientes.Add(new Cliente()
            {
                IdCliente = 2,
                NmClie = "Ronaldo Teste",
                CpfClie = "24192979381",
                SenhaClie = "12345678",
                IdEndereco = 2
            });
            dbContext.Atividades.Add(new Atividades()
            {
                IdAtividades = 1,
                DemandaProduto = "Al",
                QntdProcura = 12,
                PrecoVariado = 233,
                ClimaAtual = "Calor",
                HorarioAtual = DateTime.Now,
                LocalizacaoAtual = "Av Paulista",
                IdCliente = 1
                
            });
            dbContext.Atividades.Add(new Atividades()
            {
                IdAtividades = 2, 
                DemandaProduto = "Al",
                QntdProcura = 25,
                PrecoVariado = 150.50m, 
                ClimaAtual = "Frio",
                HorarioAtual = DateTime.Now, 
                LocalizacaoAtual = "Rua da Consolação",
                IdCliente = 2 
            });
            dbContext.Compras.Add(new Compra()
            {
                IdCompra = 1,
                NmProd = "Tenis Adidas",
                EspcificacoesProd = "Tenis para corrida",
                QntdProd = 2,
                ValorCompra = 900,
                IdProd = 1,
                IdCliente = 1,
                IdAtividades = 1
                
            });
            dbContext.Compras.Add(new Compra()
            {
                IdCompra = 2,
                NmProd = "Tenis Nike",
                EspcificacoesProd = "Tenis esportivo de alto performance",
                QntdProd = 1,
                ValorCompra = 750,
                IdProd = 2,
                IdCliente = 2,
                IdAtividades = 2
                
            });
            dbContext.SaveChanges();
        }
    }
}