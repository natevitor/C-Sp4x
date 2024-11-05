using Microsoft.EntityFrameworkCore;
using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Api.Service;
using SavvyfixAspNet.Data;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Configuration.Routes;

public static class ClienteEndpoint
{
    public static void MapClienteEndpoints(this WebApplication app)
    {
         var clientesGroup = app.MapGroup("/clientes");

            // GET: Retorna todos os clientes
            clientesGroup.MapGet("/", async (SavvyfixMetadataDbContext dbContext) =>
            {
                var clientes = await dbContext.Clientes.Include(c => c.Endereco).ToListAsync();
                return clientes.Any() ? Results.Ok(clientes) : Results.NotFound();
            })
            .WithName("Buscar clientes")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "GetClientes",
                Summary = "Retorna todos os clientes",
                Description = "Retorna todos os clientes do banco de dados",
                Deprecated = false
            })
            .Produces<List<Cliente>>()
            .Produces(StatusCodes.Status404NotFound);

            // GET: Retorna um cliente pelo ID
            clientesGroup.MapGet("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
            {
                var cliente = await dbContext.Clientes
                    .Include(c => c.Endereco)
                    .Include(c => c.Compras)
                    .Include(c => c.Atividades)
                    .FirstOrDefaultAsync(c => c.IdCliente == id);

                return cliente is not null ? Results.Ok(cliente) : Results.NotFound();
            })
            .WithName("Buscar cliente pelo id")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "GetClienteById",
                Summary = "Retorna o cliente buscado pelo ID",
                Description = "Retorna o cliente buscado pelo ID do banco de dados",
                Deprecated = false
            })
            .Produces<Cliente>()
            .Produces(StatusCodes.Status404NotFound);

            // POST: Adiciona um novo cliente
            clientesGroup.MapPost("/", async (ClienteAddOrUpdateModel clienteModel, SavvyfixMetadataDbContext dbContext) =>
                {
                    // Verifica se o modelo é válido
                    if (!clienteModel.IsValid(out var validationErrors))
                    {
                        return Results.BadRequest(validationErrors); // Retorna os erros de validação
                    }
                    
                    Endereco endereco = await dbContext.Enderecos.FindAsync(clienteModel.IdEndereco);
                    if (endereco is null)
                    {
                        return Results.BadRequest("Endereço não encontrado");
                    }
                
                dbContext.Clientes.Add(clienteModel.MapToCliente());
                await dbContext.SaveChangesAsync();

                return Results.Created($"/clientes", clienteModel);
            })
            .WithName("Adicionar novo cliente")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "AddCliente",
                Summary = "Adiciona um novo cliente",
                Description = "Adiciona um novo cliente ao banco de dados",
                Deprecated = false
            })
            .Accepts<ClienteAddOrUpdateModel>("application/json")
            .Produces<Cliente>(StatusCodes.Status201Created);

            // PUT: Atualiza um cliente existente
            clientesGroup.MapPut("/{id:long}", async (long id, ClienteAddOrUpdateModel updateModel, SavvyfixMetadataDbContext dbContext) =>
            {
                var existingCliente = await dbContext.Clientes.FindAsync(id);

                if (existingCliente is null)
                {
                    return Results.NotFound();
                }

                existingCliente.CpfClie = updateModel.CpfClie ?? existingCliente.CpfClie;
                existingCliente.NmClie = updateModel.NmClie ?? existingCliente.NmClie;
                existingCliente.SenhaClie = updateModel.SenhaClie ?? existingCliente.SenhaClie;
                existingCliente.IdEndereco = updateModel.IdEndereco ?? existingCliente.IdEndereco;

                await dbContext.SaveChangesAsync();

                return Results.Ok(existingCliente);
            })
            .WithName("Atualizar cliente")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "UpdateCliente",
                Summary = "Atualiza um cliente existente",
                Description = "Atualiza um cliente existente no banco de dados pelo ID",
                Deprecated = false
            })
            .Accepts<ClienteAddOrUpdateModel>("application/json")
            .Produces<Cliente>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE: Deleta um cliente existente
            clientesGroup.MapDelete("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
            {
                var cliente = await dbContext.Clientes.FindAsync(id);

                if (cliente is null)
                {
                    return Results.NotFound();
                }

                dbContext.Clientes.Remove(cliente);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            })
            .WithName("Deletar cliente")
            .WithOpenApi(operation => new(operation)
            {
                OperationId = "DeleteCliente",
                Summary = "Deleta um cliente existente",
                Description = "Deleta um cliente existente no banco de dados pelo ID",
                Deprecated = false
            })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }
}