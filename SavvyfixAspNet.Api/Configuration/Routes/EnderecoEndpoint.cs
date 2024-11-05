using Microsoft.EntityFrameworkCore;
using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Api.Service;
using SavvyfixAspNet.Data;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Configuration.Routes;

public static class EnderecoEndpoint
{
    public static void MapEnderecoEndpoints(this WebApplication app)
    {
         var enderecosGroup = app.MapGroup("/enderecos");

            // GET: Retorna todos os endereços
            enderecosGroup.MapGet("/", async (SavvyfixMetadataDbContext dbContext) =>
            {
                var enderecos = await dbContext.Enderecos.ToListAsync();
                return enderecos.Any() ? Results.Ok(enderecos) : Results.NotFound();
            })
            .WithName("Buscar endereços")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "GetEnderecos",
                Summary = "Retorna todos os endereços",
                Description = "Retorna todos os endereços do banco de dados",
                Deprecated = false
            })
            .Produces<List<Endereco>>()
            .Produces(StatusCodes.Status404NotFound);

            // GET: Retorna um endereço pelo ID
            enderecosGroup.MapGet("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
            {
                var endereco = await dbContext.Enderecos.FindAsync(id);
                return endereco is not null ? Results.Ok(endereco) : Results.NotFound();
            })
            .WithName("Buscar endereço pelo id")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "GetEnderecoById",
                Summary = "Retorna o endereço buscado pelo ID",
                Description = "Retorna o endereço buscado pelo ID do banco de dados",
                Deprecated = false
            })
            .Produces<Endereco>()
            .Produces(StatusCodes.Status404NotFound);

            // POST: Adiciona um novo endereço
            enderecosGroup.MapPost("/", async (EnderecoAddOrUpdateModel enderecoModel, SavvyfixMetadataDbContext dbContext) =>
                {
                    // Verifica se o modelo é válido
                    if (!enderecoModel.IsValid( out var validationErrors))
                    {
                        return Results.BadRequest(validationErrors);
                    }

                    // Mapeia o modelo de adição para a entidade
                    var endereco = new Endereco
                    {
                        CepEndereco = enderecoModel.CepEndereco,
                        RuaEndereco = enderecoModel.RuaEndereco,
                        NumEndereco = enderecoModel.NumEndereco,
                        BairroEndeereco = enderecoModel.BairroEndeereco,
                        CidadeEndereco = enderecoModel.CidadeEndereco,
                        EstadoEndereco = enderecoModel.EstadoEndereco,
                        PaisEndereco = enderecoModel.PaisEndereco
                    };

                    // Adiciona o novo endereço ao contexto do banco de dados
                    dbContext.Enderecos.Add(endereco);
                    await dbContext.SaveChangesAsync();

                    // Retorna a resposta de criação com o novo endereço
                    return Results.Created($"/enderecos/{endereco.IdEndereco}", endereco);
                })
                .WithName("Adicionar novo endereço")
                .WithOpenApi(operation => new(operation)
                {
                    OperationId = "AddEndereco",
                    Summary = "Adiciona um novo endereço",
                    Description = "Adiciona um novo endereço ao banco de dados",
                    Deprecated = false
                })
                .Accepts<EnderecoAddOrUpdateModel>("application/json")
                .Produces<Endereco>(StatusCodes.Status201Created);

            // PUT: Atualiza um endereço existente
            enderecosGroup.MapPut("/{id:long}", async (long id, EnderecoAddOrUpdateModel updateModel, SavvyfixMetadataDbContext dbContext) =>
            {
                var existingEndereco = await dbContext.Enderecos.FindAsync(id);

                if (existingEndereco is null)
                {
                    return Results.NotFound();
                }

                existingEndereco.CepEndereco = updateModel.CepEndereco ?? existingEndereco.CepEndereco;
                existingEndereco.RuaEndereco = updateModel.RuaEndereco ?? existingEndereco.RuaEndereco;
                existingEndereco.NumEndereco = updateModel.NumEndereco ?? existingEndereco.NumEndereco;
                existingEndereco.BairroEndeereco = updateModel.BairroEndeereco ?? existingEndereco.BairroEndeereco;
                existingEndereco.CidadeEndereco = updateModel.CidadeEndereco ?? existingEndereco.CidadeEndereco;
                existingEndereco.EstadoEndereco = updateModel.EstadoEndereco ?? existingEndereco.EstadoEndereco;
                existingEndereco.PaisEndereco = updateModel.PaisEndereco ?? existingEndereco.PaisEndereco;

                await dbContext.SaveChangesAsync();

                return Results.Ok(existingEndereco);
            })
            .WithName("Atualizar endereço")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "UpdateEndereco",
                Summary = "Atualiza um endereço existente",
                Description = "Atualiza um endereço existente no banco de dados pelo ID",
                Deprecated = false
            })
            .Accepts<EnderecoAddOrUpdateModel>("application/json")
            .Produces<Endereco>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE: Deleta um endereço existente
            enderecosGroup.MapDelete("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
            {
                var endereco = await dbContext.Enderecos.FindAsync(id);

                if (endereco is null)
                {
                    return Results.NotFound();
                }

                dbContext.Enderecos.Remove(endereco);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("Deletar endereço")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "DeleteEndereco",
                Summary = "Deleta um endereço existente",
                Description = "Deleta um endereço existente no banco de dados pelo ID",
                Deprecated = false
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
