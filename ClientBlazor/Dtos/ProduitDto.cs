namespace ClientBlazor.Dtos;

public class ProduitDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Marque { get; set; } = null!;
}
