namespace SavvyfixAspNet.Api.Models;

public record CompraAddOrUpdateModel
{
    public int QntdProd { get; set; }
    public long? IdProd { get; set; }
    public long? IdCliente { get; set; }
    public long? IdAtividades { get; set; }
    public string NmProd { get; set; }
    public string EspcificacoesProd { get; set; }

    // Validação do modelo
    public bool IsValid(out List<string> validationErrors)
    {
        validationErrors = new List<string>();

        if (QntdProd <= 0)
        {
            validationErrors.Add("A quantidade do produto deve ser maior que zero.");
        }

        if (IdProd == null)
        {
            validationErrors.Add("O ID do produto é obrigatório.");
        }

        if (IdCliente == null)
        {
            validationErrors.Add("O ID do cliente é obrigatório.");
        }

        if (IdAtividades == null)
        {
            validationErrors.Add("O ID de atividades é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(NmProd))
        {
            validationErrors.Add("O nome do produto é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(EspcificacoesProd))
        {
            validationErrors.Add("As especificações do produto são obrigatórias.");
        }

        return !validationErrors.Any(); 
    }
}