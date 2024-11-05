public class ProdutoAddOrUpdateModel
{
    public string NmProd { get; set; }
    public string DescProd { get; set; }
    public string MarcaProd { get; set; }
    public decimal PrecoFixo { get; set; }
    public string Img { get; set; }

    // Validação do modelo
    public bool IsValid(out List<string> validationErrors)
    {
        validationErrors = new List<string>();
        
        if (string.IsNullOrWhiteSpace(NmProd))
        {
            validationErrors.Add("O nome do produto é obrigatório.");
        }
        
        if (string.IsNullOrWhiteSpace(DescProd))
        {
            validationErrors.Add("A descrição do produto é obrigatória.");
        }

        if (string.IsNullOrWhiteSpace(MarcaProd))
        {
            validationErrors.Add("A marca do produto é obrigatória.");
        }

        if (PrecoFixo <= 0)
        {
            validationErrors.Add("O preço deve ser um valor positivo.");
        }

        if (string.IsNullOrWhiteSpace(Img))
        {
            validationErrors.Add("A imagem do produto é obrigatória.");
        }

        return !validationErrors.Any(); // Retorna verdadeiro se não houver erros
    }
}