using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Service;

public static class AtividadesService
{
    public static Atividades MapToAtv(this AtividadesAddOrUpdateModel atividadeModel)
    {
        return new Atividades()
        {
            ClimaAtual = atividadeModel.ClimaAtual,
            DemandaProduto = atividadeModel.DemandaProduto,
            HorarioAtual = atividadeModel.HorarioAtual,
            LocalizacaoAtual = atividadeModel.LocalizacaoAtual,
            PrecoVariado = atividadeModel.PrecoVariado,
            QntdProcura = atividadeModel.QntdProcura,
            IdCliente = atividadeModel.IdCliente

        };
    }
}