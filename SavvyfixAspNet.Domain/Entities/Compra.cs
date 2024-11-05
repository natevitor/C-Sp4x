using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SavvyfixAspNet.Domain.Entities;

public class Compra
{
    [Key]
    [Column("id_compra")]
    public long IdCompra { get; set; }
    
    [Required(ErrorMessage = "A quantidade de procura é obrigatória.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que 0.")]
    [Column("qntd_prod")]
    public int QntdProd { get; set; }
    
    [Required(ErrorMessage = "O valor da compra é obrigatória.")]
    [Column("valor_compra")]
    public decimal ValorCompra { get; set; }
    
    [Required(ErrorMessage = "O produto é obrigatório.")]
    [Column("id_produto")]
    public long? IdProd { get; set; }
    
    [Required(ErrorMessage = "O cliente é obrigatório.")]
    [Column("id_cliente")]
    public long? IdCliente { get; set; }
    
    [Column("id_atividades")]
    public long? IdAtividades { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [Column("nm_prod")]
    public string NmProd { get; set; }
    
    [Required(ErrorMessage = "As especificações do produto são é obrigatórias.")]
    [Column("especificacao_prod")]
    public string EspcificacoesProd { get; set; }
    
    
    public Produto Produto { get; set; }
    
    
    public Cliente Cliente { get; set; }
    
    public Atividades Atividades { get; set; }
}