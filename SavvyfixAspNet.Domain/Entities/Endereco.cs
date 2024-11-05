using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SavvyfixAspNet.Domain.Entities;

public class Endereco
{
    [Key]
    [Column("id_endereco")]
    public long IdEndereco { get; set; }
    
    [Required(ErrorMessage = "O cep é obrigatório.")]
    [Column("cep_endereco")]
    [StringLength(8, MinimumLength = 8, ErrorMessage = "O CEP deve ter exatamente 8 números.")]
    public string CepEndereco { get; set; }

    [Required(ErrorMessage = "A rua é obrigatória.")]
    [Column("rua_endereco")]
    public string RuaEndereco { get; set; }
        
    [Required(ErrorMessage = "O número é obrigatório.")]
    [Column("num_endereco")]
    public string NumEndereco { get; set; }
    
    [Required(ErrorMessage = "O bairro é obrigatório.")]
    [Column("bairro_endereco")]
    public string BairroEndeereco { get; set; }
    
    [Required(ErrorMessage = "A cidade é obrigatória.")]
    [Column("cidade_endereco")]
    public string CidadeEndereco { get; set; }
    
    [Required(ErrorMessage = "O endereco é obrigatório.")]
    [Column("estado_endereco")]
    [StringLength(2, MinimumLength = 2, ErrorMessage = "Apenas as siglas do estado.")]
    public string EstadoEndereco { get; set; }
    
    [Required(ErrorMessage = "O pais é obrigatório.")]
    [Column("pais")]
    public string PaisEndereco { get; set; }
    
    [JsonIgnore]
    public ICollection<Cliente> Clientes { get; set; }
}