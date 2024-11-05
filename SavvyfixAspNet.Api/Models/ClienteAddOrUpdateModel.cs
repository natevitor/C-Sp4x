using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SavvyfixAspNet.Api.Models;

[Serializable]
public record ClienteAddOrUpdateModel
{
    public string CpfClie { get; set; } = null!;
    
    public string NmClie { get; set; } = null!;
    
    public string SenhaClie { get; set; } = null!;
    
    public long? IdEndereco { get; set; }
    
    // Validação do modelo
    public bool IsValid(out List<string> validationErrors)
    {
        validationErrors = new List<string>();

        if (string.IsNullOrWhiteSpace(CpfClie) || !Regex.IsMatch(CpfClie, @"^\d{11}$"))
        {
            validationErrors.Add("O CPF deve ter 11 dígitos e é obrigatório.");
        }
        
        if (string.IsNullOrWhiteSpace(NmClie))
        {
            validationErrors.Add("O nome do cliente é obrigatório.");
        }

        if (string.IsNullOrWhiteSpace(SenhaClie) || SenhaClie.Length < 6)
        {
            validationErrors.Add("A senha é obrigatória e deve ter pelo menos 6 caracteres.");
        }

        if (!IdEndereco.HasValue || IdEndereco <= 0)
        {
            validationErrors.Add("O ID do endereço deve ser um valor positivo e obrigatório.");
        }

        return !validationErrors.Any(); // Retorna verdadeiro se não houver erros
    }
}