public class AtividadesAddOrUpdateModel
{
    public string? ClimaAtual { get; set; }
    
    public string DemandaProduto { get; set; }
    
    public DateTime? HorarioAtual { get; set; }
    
    public string? LocalizacaoAtual { get; set; }
    
    public decimal PrecoVariado { get; set; }
    
    public int QntdProcura { get; set; }
    
    public long? IdCliente { get; set; }

    // Validação do modelo
    public bool IsValid(out List<string> validationErrors)
    {
        validationErrors = new List<string>();

        if (string.IsNullOrWhiteSpace(DemandaProduto))
        {
            validationErrors.Add("A demanda do produto é obrigatória.");
        }

        if (PrecoVariado < 0)
        {
            validationErrors.Add("O preço deve ser um valor não negativo.");
        }

        if (QntdProcura < 0)
        {
            validationErrors.Add("A quantidade de procura deve ser um valor não negativo.");
        }

        if (IdCliente == null)
        {
            validationErrors.Add("O ID do cliente é obrigatório.");
        }

        return !validationErrors.Any(); 
    }
}