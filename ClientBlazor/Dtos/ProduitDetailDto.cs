namespace ClientBlazor.Dtos;

public class ProduitDetailDto
{
    public int Id { get; set; }
    public string Nom { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Marque { get; set; } = null!;
    public string? Description { get; set; }
    public string? Nomphoto { get; set; }
    public string? Uriphoto { get; set; }
    public int? Stock { get; set; }
    public bool EnReappro { get; set; }
}
