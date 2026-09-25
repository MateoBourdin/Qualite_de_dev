using AutoMapper;
using TPRevision.Entities;
using TPRevision.Models.Model.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Marque, MarqueDto>();
        CreateMap<TypeProduit, TypeProduitDto>();
    }
}