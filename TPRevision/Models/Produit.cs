using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Intrinsics.X86;

[PrimaryKey("IdProduit")]
[Table("t_e_produit_pro")]
public partial class Produit
{
    [Key]
    [Column("idProduit")]
    public int IdProduit
    {
        get; set;
    }

    [Column("nomProduit")]
    [StringLength(50)]
    public string nomProduit { get; set; } = null!;


    [Column("description")]
    [StringLength(50)]
    public string Description { get; set; } = null!;

    [Column("nomPhoto")]
    [StringLength(50)]
    public string NomPhoto { get; set; } = null!;

    [Column("uriPhoto")]
    [StringLength(50)]
    public string UriPhoto { get; set; } = null!;

    [Column("idMarque")]
    public int IdMarque
    {
        get; set;
    }

    [Column("idTypeProduit")]
    public int IdTypeProduit
    {
        get; set;
    }

    [ForeignKey("idTypeProduit")]
    [InverseProperty(nameof(TypeProduit.produits))]
    //[NavigationProperty]
    public virtual TypeProduit TypeProduitNavigation { get; set; } = null!;

    [ForeignKey("idMarque")]
    [InverseProperty(nameof(Marque.produits))]
    //[NavigationProperty]
    public virtual Marque MarqueNavigation { get; set; } = null!;

    [Column("StockReel")]
    public int? stockReel { get; set; } = null!;
    [Column("StockMin")]
    public int? stockMin { get; set; } = null!;
    [Column("StockMax")]
    public int? stockMax { get; set; } = null!;


}