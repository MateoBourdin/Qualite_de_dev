
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[PrimaryKey("IdTypeProduit")]
[Table("t_e_typeproduit_typ")]
public partial class TypeProduit
{
    [Key]
    [Column("idTypeProduit")]
    public int IdTypeProduit
    {
        get; set;
    }

    [Column("nomTypeProduit")]
    [StringLength(50)]
    public string NomTypeProduit { get; set; } = null!;

    [InverseProperty(nameof(Produit.TypeProduitNavigation))]
    public ICollection<Produit>? produits { get; set; } = new List<Produit>();


}