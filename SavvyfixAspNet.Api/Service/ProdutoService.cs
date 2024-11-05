using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Service;

public static class ProdutoService
{
    public static Produto MapToPro(this ProdutoAddOrUpdateModel model)
    {
        return new Produto()
        {
            NmProd = model.NmProd,
            MarcaProd = model.MarcaProd,
            DescProd = model.DescProd,
            PrecoFixo = model.PrecoFixo,
            Img = model.Img
        };
    }
}