using Microsoft.EntityFrameworkCore;
using SavvyfixAspNet.Api.Configuration.Routes;
using SavvyfixAspNet.Data;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddDbContext<SavvyfixMetadataDbContext>(options =>
        {
            options.UseOracle(builder.Configuration.GetConnectionString("FiapOracleConnection"));
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.MapProdutoEndpoints();
        app.MapEnderecoEndpoints();
        app.MapClienteEndpoints();
        app.MapAtividadesEndpoints();
        app.MapCompraEndpoint();

        app.Run();
    }
}