using AutoMapper;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Marque, MarqueDto>()
            .ForMember(dest => dest.NbProduits, opt => opt.MapFrom(src => src.produits!.Count()));

        CreateMap<TypeProduit, TypeProduitDto>()
            .ForMember(dest => dest.NbProduits, opt => opt.MapFrom(src => src.produits!.Count()));

        CreateMap<Produit, ProduitDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdProduit))
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.NomProduit))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TypeProduitNavigation!.NomTypeProduit))
            .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.MarqueNavigation!.NomMarque));

        CreateMap<Produit, ProduitDetailDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdProduit))
            .ForMember(dest => dest.Nom, opt => opt.MapFrom(src => src.NomProduit))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.TypeProduitNavigation!.NomTypeProduit))
            .ForMember(dest => dest.Marque, opt => opt.MapFrom(src => src.MarqueNavigation!.NomMarque))
            .ForMember(dest => dest.Nomphoto, opt => opt.MapFrom(src => src.NomPhoto))
            .ForMember(dest => dest.Uriphoto, opt => opt.MapFrom(src => src.UriPhoto))
            .ForMember(dest => dest.Stock, opt => opt.MapFrom(src => src.stockReel))
            // En SQL, comparer avec NULL donne NULL (et non false) : on teste explicitement les valeurs nulles
            .ForMember(dest => dest.EnReappro, opt => opt.MapFrom(src => src.stockReel != null && src.stockMin != null && src.stockReel < src.stockMin));
    }
}