using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Service;

public static class ClienteService
{
    public static Cliente MapToCliente(this ClienteAddOrUpdateModel clienteModel)
    {
        return new Cliente()
        {
            CpfClie = clienteModel.CpfClie,
            NmClie = clienteModel.NmClie,
            SenhaClie = clienteModel.SenhaClie,
            IdEndereco = clienteModel.IdEndereco
        };
    }
}