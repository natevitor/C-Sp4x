using Microsoft.EntityFrameworkCore;
using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Api.Service;
using SavvyfixAspNet.Data;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Configuration.Routes;

public static class AtividadesEndpoint
{
    public static void MapAtividadesEndpoints(this WebApplication app)
    {
        var atividadesGroup = app.MapGroup("/atividades");

        // GET: Retorna todas as atividades
        atividadesGroup.MapGet("/", async (SavvyfixMetadataDbContext dbContext) =>
        {
            var atividades = await dbContext.Atividades
                .Include(a => a.Cliente)
                .ToListAsync();
            return atividades.Any() ? Results.Ok(atividades) : Results.NotFound();
        })
        .WithName("Buscar atividades")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "GetAtividades",
            Summary = "Retorna todas as atividades",
            Description = "Retorna todas as atividades do banco de dados",
            Deprecated = false
        })
        .Produces<List<Atividades>>()
        .Produces(StatusCodes.Status404NotFound);

        // GET: Retorna uma atividade pelo ID
        atividadesGroup.MapGet("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
        {
            var atividade = await dbContext.Atividades
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(a => a.IdAtividades == id);

            return atividade is not null ? Results.Ok(atividade) : Results.NotFound();
        })
        .WithName("Buscar atividade pelo id")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "GetAtividadesById",
            Summary = "Retorna uma atividade pelo ID",
            Description = "Retorna a atividade pelo ID do banco de dados",
            Deprecated = false
        })
        .Produces<Atividades>()
        .Produces(StatusCodes.Status404NotFound);

        // POST: Adiciona uma nova atividade
        atividadesGroup.MapPost("/", async (AtividadesAddOrUpdateModel atividadeModel, SavvyfixMetadataDbContext dbContext) =>
        {

            // Verifica se o modelo é válido
            if (!atividadeModel.IsValid(out var validationErrors))
            {
                return Results.BadRequest(validationErrors); // Retorna os erros de validação
            }

            Cliente cliente = await dbContext.Clientes.FindAsync(atividadeModel.IdCliente);
            if (cliente is null)
            {
                return Results.BadRequest("Cliente não encontrado");
            }
            
            dbContext.Atividades.Add(atividadeModel.MapToAtv());
            await dbContext.SaveChangesAsync();

            return Results.Created($"/atividades", atividadeModel);
        })
        .WithName("Adicionar nova atividade")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "AddAtividades",
            Summary = "Adiciona uma nova atividade",
            Description = "Adiciona uma nova atividade ao banco de dados",
            Deprecated = false
        })
        .Accepts<AtividadesAddOrUpdateModel>("application/json")
        .Produces<Atividades>(StatusCodes.Status201Created);

        // PUT: Atualiza uma atividade existente
        atividadesGroup.MapPut("/{id:long}", async (long id, AtividadesAddOrUpdateModel updateModel, SavvyfixMetadataDbContext dbContext) =>
        {
            var existingAtividade = await dbContext.Atividades.FindAsync(id);

            if (existingAtividade is null)
            {
                return Results.NotFound();
            }

            existingAtividade.ClimaAtual = updateModel.ClimaAtual ?? existingAtividade.ClimaAtual;
            existingAtividade.DemandaProduto = updateModel.DemandaProduto ?? existingAtividade.DemandaProduto;
            existingAtividade.HorarioAtual = updateModel.HorarioAtual ?? existingAtividade.HorarioAtual;
            existingAtividade.LocalizacaoAtual = updateModel.LocalizacaoAtual ?? existingAtividade.LocalizacaoAtual;
            existingAtividade.IdCliente = updateModel.IdCliente ?? existingAtividade.IdCliente;
            if (updateModel.PrecoVariado != 0)
            {
                existingAtividade.PrecoVariado = updateModel.PrecoVariado;
            }

            if (updateModel.QntdProcura != 0)
            {
                existingAtividade.QntdProcura = updateModel.QntdProcura;
            }


            await dbContext.SaveChangesAsync();

            return Results.Ok(existingAtividade);
        })
        .WithName("Atualizar atividade")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "UpdateAtividades",
            Summary = "Atualiza uma atividade existente",
            Description = "Atualiza uma atividade existente no banco de dados pelo ID",
            Deprecated = false
        })
        .Accepts<AtividadesAddOrUpdateModel>("application/json")
        .Produces<Atividades>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // DELETE: Deleta uma atividade existente
        atividadesGroup.MapDelete("/{id:long}", async (long id, SavvyfixMetadataDbContext dbContext) =>
        {
            var atividade = await dbContext.Atividades.FindAsync(id);

            if (atividade is null)
            {
                return Results.NotFound();
            }

            dbContext.Atividades.Remove(atividade);
            await dbContext.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("Deletar atividade")
        .WithOpenApi(operation => new(operation)
        {
            OperationId = "DeleteAtividades",
            Summary = "Deleta uma atividade existente",
            Description = "Deleta uma atividade existente no banco de dados pelo ID",
            Deprecated = false
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);
    }
}