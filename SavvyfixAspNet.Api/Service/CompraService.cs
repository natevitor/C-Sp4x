using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Data;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Service;

public static class CompraService
{
    public static async Task<Compra> MapToCompra(this CompraAddOrUpdateModel compraModel, SavvyfixMetadataDbContext dbContext)
    {
        Atividades atividades = await dbContext.Atividades.FindAsync(compraModel.IdAtividades);
        var precoVariado = atividades.PrecoVariado;

        Produto produto = await dbContext.Produtos.FindAsync(compraModel.IdProd);
        var valorProd = produto.PrecoFixo;
        
        if (atividades != null)
        {
            var valorTotal = precoVariado * compraModel.QntdProd;
            return new Compra()
            {
                QntdProd = compraModel.QntdProd,
                IdProd = compraModel.IdProd,
                IdCliente = compraModel.IdCliente,
                IdAtividades = compraModel.IdAtividades,
                NmProd = compraModel.NmProd,
                EspcificacoesProd = compraModel.EspcificacoesProd,
                ValorCompra = valorTotal
            };
        }
        
        return new Compra()
        {
            QntdProd = compraModel.QntdProd,
            IdProd = compraModel.IdProd,
            IdCliente = compraModel.IdCliente,
            IdAtividades = compraModel.IdAtividades,
            NmProd = compraModel.NmProd,
            EspcificacoesProd = compraModel.EspcificacoesProd,
            ValorCompra = valorProd * compraModel.QntdProd
        };
    }
    
}