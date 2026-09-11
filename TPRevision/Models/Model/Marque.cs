using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[PrimaryKey("IdMarque")]
[Table("marque")]
public partial class Marque
{
    [Key]
    [Column("idmarque")]
    public int IdMarque { get; set; }

    [Column("nomMarque")]
    [StringLength(50)]
    public string NomMarque { get; set; } = null!;

    [InverseProperty(nameof(Produit.MarqueNavigation))]
    public ICollection<Produit>? produits { get; set; } = new List<Produit>();

}