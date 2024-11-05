using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SavvyfixAspNet.Domain.Entities;

public class Cliente
{
    [Key]
    [Column("id_cliente")]
    public long IdCliente { get; set; }

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [Column("cpf_clie")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve ter exatamente 11 números.")]
    public string CpfClie { get; set; } = null!;

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [Column("nm_clie")]
    public string NmClie { get; set; } = null!;

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [Column("senha_clie")]
    public string SenhaClie { get; set; } = null!;
    
    [Required(ErrorMessage = "O endereco é obrigatório.")]
    [Column("id_endereco")]
    public long? IdEndereco { get; set; }
    
    
    public Endereco Endereco { get; set; }
    
    [JsonIgnore]
    public ICollection<Compra> Compras { get; set; }
    
    [JsonIgnore]
    public ICollection<Atividades> Atividades { get; set; }
}