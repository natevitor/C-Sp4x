using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SavvyfixAspNet.Domain.Entities;

public class Atividades
{

    [Key]
    [Column("id_atividades")]
    public long IdAtividades { get; set; }

    [Column("clima_atual")]
    public string? ClimaAtual { get; set; }

    [Column("demanda_produto")]
    public string DemandaProduto { get; set; }

    [Column("horario_atual")]
    public DateTime? HorarioAtual { get; set; }

    [Column("localizacao_atual")]
    public string? LocalizacaoAtual { get; set; }

    [Column("preco_variado")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    public decimal PrecoVariado { get; set; }

    [Column("qntd_procura")]
    public int QntdProcura { get; set; }
    
    [Required(ErrorMessage = "O cliente é obrigatório.")]
    [Column("id_cliente")]
    public long? IdCliente { get; set; }
    
    public Cliente Cliente { get; set; }
    
    [JsonIgnore]
    public ICollection<Compra> Compras { get; set; }

    

}