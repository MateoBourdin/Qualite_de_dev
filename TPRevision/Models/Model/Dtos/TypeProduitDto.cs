using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPRevision.Models.Model.Dtos
{
    public class TypeProduitDto
    {


        public int IdTypeProduit
        {
            get; set;
        }

        public string NomTypeProduit { get; set; } = null!;

        public int NbProduits { get; set; }

    }
}
