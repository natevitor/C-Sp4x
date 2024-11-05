using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SavvyfixAspNet.Domain.Entities;

public class Produto
{
    [Key]
    [Column("id_prod")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IdProd { get; set; }
    
    [Required(ErrorMessage = "O preço fixo é obrigatório")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
    [Column("preco_fixo")]
    public decimal PrecoFixo { get; set; }
    
    [Required(ErrorMessage = "A marca é obrigatória.")]
    [Column("marca_prod")]
    public string MarcaProd { get; set; } = null!;

    [Required(ErrorMessage = "A descrição do produto é obrigatório.")]
    [Column("desc_proc")]
    public string DescProd { get; set; } = null!;

    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [Column("nm_prod")]
    public string NmProd { get; set; } = null!;
    
    [Required(ErrorMessage = "A imagem do produto é obrigatória.")]
    [Column("img_prod")]
    public string Img { get; set; } 
    
    [JsonIgnore]
    public ICollection<Compra> Compras { get; set; }
}