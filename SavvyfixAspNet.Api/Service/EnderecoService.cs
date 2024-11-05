using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Service;

public static class EnderecoService
{
    public static Endereco MapToEndereco(this EnderecoAddOrUpdateModel enderecoModel)
    {
        return new Endereco()
        {
            CepEndereco = enderecoModel.CepEndereco,
            RuaEndereco = enderecoModel.RuaEndereco,
            NumEndereco = enderecoModel.NumEndereco,
            BairroEndeereco = enderecoModel.BairroEndeereco,
            CidadeEndereco = enderecoModel.CidadeEndereco,
            EstadoEndereco = enderecoModel.EstadoEndereco,
            PaisEndereco = enderecoModel.PaisEndereco
        };
    }
}