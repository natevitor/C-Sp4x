using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SavvyfixAspNet.Api.Models;

[Serializable]
public record EnderecoAddOrUpdateModel
{
    public string CepEndereco { get; set; }

    public string RuaEndereco { get; set; }

    public string NumEndereco { get; set; }

    public string BairroEndeereco { get; set; }

    public string CidadeEndereco { get; set; }
    
    public string EstadoEndereco { get; set; }
    
    public string PaisEndereco { get; set; }

    // Validação do modelo
    public bool IsValid(out List<string> validationErrors)
    {
        validationErrors = new List<string>();

        // Verificação de cada campo, similar ao ProdutoAddOrUpdateModel
        if (string.IsNullOrWhiteSpace(CepEndereco) || CepEndereco.Length != 8)
        {
            validationErrors.Add("O CEP deve ter exatamente 8 números e é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(RuaEndereco))
        {
            validationErrors.Add("A rua é obrigatória.");
        }

        if (string.IsNullOrWhiteSpace(NumEndereco))
        {
            validationErrors.Add("O número é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(BairroEndeereco))
        {
            validationErrors.Add("O bairro é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(CidadeEndereco))
        {
            validationErrors.Add("A cidade é obrigatória.");
        }

        if (string.IsNullOrWhiteSpace(EstadoEndereco) || EstadoEndereco.Length != 2)
        {
            validationErrors.Add("O estado é obrigatório e deve ter exatamente 2 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(PaisEndereco))
        {
            validationErrors.Add("O país é obrigatório.");
        }

        return !validationErrors.Any(); // Retorna verdadeiro se não houver erros
    }
}
