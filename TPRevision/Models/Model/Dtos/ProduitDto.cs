namespace TPRevision.Models.Model.Dtos
{
    public class ProduitDto
    {
        public int Id { get; set; }
        public string? Nom { get; set; }
        public string? Type { get; set; }   // nom du TypeProduit, pas l'id
        public string? Marque { get; set; } // nom de la Marque, pas l'id
    }
}
