using Microsoft.EntityFrameworkCore;
using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Api.Service;
using SavvyfixAspNet.Data;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Configuration.Routes;

public static class CompraEndpoint
{
    public static void MapCompraEndpoint(this WebApplication app)
    {
        var comprasGroup = app.MapGroup("/compras");

        // GET: Retorna todas as compras
        comprasGroup.MapGet("/", async (SavvyfixMetadataDbContext dbContext) =>
        {
            var compras = await dbContext.Compras
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .Include(c => c.Atividades)
                .ToListAsync();
                
            return compras.Any() ? Results.Ok(compras) : Results.NotFound();
        })
        .WithName("Buscar compras")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "GetCompras",
            Summary = "Retorna todas as compras",
            Description = "Retorna todas as compras do banco de dados",
            Deprecated = false
        })
        .Produces<List<Compra>>()
        .Produces(StatusCodes.Status404NotFound);

        // GET: Retorna uma compra pelo ID
        comprasGroup.MapGet("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
        {
            var compra = await dbContext.Compras
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .Include(c => c.Atividades)
                .FirstOrDefaultAsync(c => c.IdCompra == id);

            return compra is not null ? Results.Ok(compra) : Results.NotFound();
        })
        .WithName("Buscar compra pelo id")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "GetCompraById",
            Summary = "Retorna a compra buscada pelo ID",
            Description = "Retorna a compra buscada pelo ID do banco de dados",
            Deprecated = false
        })
        .Produces<Compra>()
        .Produces(StatusCodes.Status404NotFound);

        // POST: Adiciona uma nova compra
        comprasGroup.MapPost("/", async (CompraAddOrUpdateModel compraModel, SavvyfixMetadataDbContext dbContext) =>
        {
            // Verifica se o modelo é válido
            if (!compraModel.IsValid( out var validationErrors))
            {
                return Results.BadRequest(validationErrors);
            }
            
            Cliente cliente = await dbContext.Clientes.FindAsync(compraModel.IdCliente);
            Produto produto = await dbContext.Produtos.FindAsync(compraModel.IdProd);
            Atividades atividades = await dbContext.Atividades.FindAsync(compraModel.IdAtividades);
            
            if (cliente is null)
            {
                return Results.BadRequest("Cliente não encontrado");
            }
            else if (produto is null)
            {
                return Results.BadRequest("Produto não encontrado");
            }
            else if (atividades is null)
            {
                return Results.BadRequest("Atividade não encontrado");
            }
            
            var compra = await compraModel.MapToCompra(dbContext);
            dbContext.Compras.Add(compra);
            await dbContext.SaveChangesAsync();

            return Results.Created($"/compras", compra);
        })
        .WithName("Adicionar nova compra")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "AddCompra",
            Summary = "Adiciona uma nova compra",
            Description = "Adiciona uma nova compra ao banco de dados",
            Deprecated = false
        })
        .Accepts<CompraAddOrUpdateModel>("application/json")
        .Produces<Compra>(StatusCodes.Status201Created);

        // PUT: Atualiza uma compra existente
        comprasGroup.MapPut("/{id:long}", async (long id, CompraAddOrUpdateModel updateModel, SavvyfixMetadataDbContext dbContext) =>
        {
            var existingCompra = await dbContext.Compras.FindAsync(id);

            if (existingCompra is null)
            {
                return Results.NotFound();
            }
            
            Atividades atividades = await dbContext.Atividades.FindAsync(updateModel.IdAtividades);
            var precoVariado = atividades.PrecoVariado;

            Produto produto = await dbContext.Produtos.FindAsync(updateModel.IdProd);
            var valorProd = produto.PrecoFixo;
            
            if (atividades != null)
            {
                var valorTotal = precoVariado * updateModel.QntdProd;
                
                existingCompra.QntdProd = updateModel.QntdProd;
                existingCompra.ValorCompra = valorTotal;
                existingCompra.IdProd = updateModel.IdProd;
                existingCompra.IdCliente = updateModel.IdCliente;
                existingCompra.IdAtividades = updateModel.IdAtividades;
                existingCompra.NmProd = updateModel.NmProd;
                existingCompra.EspcificacoesProd = updateModel.EspcificacoesProd;
                
            }
            else
            {
                existingCompra.QntdProd = updateModel.QntdProd;
                existingCompra.ValorCompra = valorProd * updateModel.QntdProd;
                existingCompra.IdProd = updateModel.IdProd;
                existingCompra.IdCliente = updateModel.IdCliente;
                existingCompra.IdAtividades = updateModel.IdAtividades;
                existingCompra.NmProd = updateModel.NmProd;
                existingCompra.EspcificacoesProd = updateModel.EspcificacoesProd;
            }

            await dbContext.SaveChangesAsync();

            return Results.Ok(existingCompra);
        })
        .WithName("Atualizar compra")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "UpdateCompra",
            Summary = "Atualiza uma compra existente",
            Description = "Atualiza uma compra existente no banco de dados pelo ID",
            Deprecated = false
        })
        .Accepts<CompraAddOrUpdateModel>("application/json")
        .Produces<Compra>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // DELETE: Deleta uma compra existente
        comprasGroup.MapDelete("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
        {
            var compra = await dbContext.Compras.FindAsync(id);

            if (compra is null)
            {
                return Results.NotFound();
            }

            dbContext.Compras.Remove(compra);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("Deletar compra")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "DeleteCompra",
            Summary = "Deleta uma compra existente",
            Description = "Deleta uma compra existente no banco de dados pelo ID",
            Deprecated = false
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}