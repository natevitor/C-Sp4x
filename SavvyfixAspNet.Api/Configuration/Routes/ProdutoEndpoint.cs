using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.WebEncoders.Testing;
using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Api.Service;
using SavvyfixAspNet.Data;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Configuration.Routes;

public static class  ProdutoEndpoint
{
    public static void MapProdutoEndpoints(this WebApplication app)
    {
        var produtosGroup = app.MapGroup("/produtos");

        // GET: Retorna todas os produtos
        produtosGroup.MapGet("/", async (SavvyfixMetadataDbContext dbcontext) =>
            {
                var produtos = await dbcontext.Produtos.ToListAsync();
                return produtos.Any() ? Results.Ok(produtos) : Results.NotFound();
            })
            .WithName("Buscar produtos")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "GetProdutos",
                Summary = "Retorna todos os produtos",
                Description = "Retorna todos os produtos",
                Deprecated = false
            })
            .Produces<List<Produto>>()
            .Produces(StatusCodes.Status404NotFound);

        // GET: Retorna um produto pelo ID
        produtosGroup.MapGet("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
            {
                var produto = await dbContext.Produtos.FindAsync(id);

                return produto is not null ? Results.Ok(produto) : Results.NotFound();
            })
            .WithName("Buscar produtos pelo id")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "GetProdutosById",
                Summary = "Retorna o produto buscado pelo id",
                Description = "Retorna o produto buscado pelo id",
                Deprecated = false
            })
            .Produces<Produto>()
            .Produces(StatusCodes.Status404NotFound);
        
        // POST: Adiciona um novo produto
        produtosGroup.MapPost("/", async (ProdutoAddOrUpdateModel produtoModel, SavvyfixMetadataDbContext dbContext) =>
            {
                // Verifica se o modelo é válido
                if (!produtoModel.IsValid(out var validationErrors))
                {
                    return Results.BadRequest(validationErrors); // Retorna os erros de validação
                }

                // Mapeia o modelo para a entidade Produto e adiciona ao contexto
                var produto = produtoModel.MapToPro();
                await dbContext.Produtos.AddAsync(produto);
                await dbContext.SaveChangesAsync();
    
                return Results.Created("/produtos", produtoModel);
            })
            .WithName("Adicionar novo produto")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "AddProduto",
                Summary = "Adiciona um novo produto",
                Description = "Adiciona um novo produto ao banco de dados",
                Deprecated = false
            })
            .Accepts<Produto>("application/json")
            .Produces<Produto>(StatusCodes.Status201Created);

        // PUT: Atualiza um endereço existente
        produtosGroup.MapPut("/{id:long}", async (long id, ProdutoAddOrUpdateModel updateModel, SavvyfixMetadataDbContext dbContext) =>
            {
               
                var existingProduto = await dbContext.Produtos.FindAsync(id);

                if (existingProduto is null)
                {
                    return Results.NotFound();
                }
                
                existingProduto.NmProd = updateModel.NmProd ?? existingProduto.NmProd;
                existingProduto.DescProd = updateModel.DescProd ?? existingProduto.DescProd;
                existingProduto.MarcaProd = updateModel.MarcaProd ?? existingProduto.MarcaProd;
                existingProduto.PrecoFixo = updateModel.PrecoFixo != 0 ? updateModel.PrecoFixo : existingProduto.PrecoFixo;
                existingProduto.Img = updateModel.Img ?? existingProduto.Img;
                
                await dbContext.SaveChangesAsync();
                
                return Results.Ok(existingProduto);
            })
            .WithName("Atualizar produto")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "UpdateProduto",
                Summary = "Atualiza um produto existente",
                Description = "Atualiza um produto existente no banco de dados pelo ID",
                Deprecated = false
            })
            .Accepts<ProdutoAddOrUpdateModel>("application/json")
            .Produces<Produto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);
        
        // DELETE: Deleta um endereço existente
        produtosGroup.MapDelete("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
            {
                var produto = await dbContext.Produtos.FindAsync(id);

                if (produto is null)
                {
                    return Results.NotFound();
                }
                dbContext.Produtos.Remove(produto);
                await dbContext.SaveChangesAsync();
                
                return Results.NoContent();
            })
            .WithName("Deletar produto")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "DeleteProduto",
                Summary = "Deleta um produto existente",
                Description = "Deleta um produto existente no banco de dados pelo ID",
                Deprecated = false
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);







    }
    
}