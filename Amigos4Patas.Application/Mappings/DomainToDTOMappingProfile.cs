using Amigos4Patas.Application.DTOs;
using Amigos4Patas.Application.DTOs.Canil;
using Amigos4Patas.Application.DTOs.Pet;
using Amigos4Patas.Domain.Entities;
using AutoMapper;

namespace Amigos4Patas.Application.Mappings;

public class DomainToDTOMappingProfile : Profile
{
    public DomainToDTOMappingProfile()
    {
        CreateMap<Pet,PetDTO>().ReverseMap();
        CreateMap<Pet,PetRetornoDTO>().ReverseMap();

        CreateMap<Canil, CanilDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.CanilID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao));
        
        CreateMap<Canil, CanilRetornoDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest=>dest.CanilID,opt=>opt.MapFrom(src=>src.Id))
            .ForMember(dest=>dest.Descricao,opt=>opt.MapFrom(src=>src.Descricao))
            .ForMember(dest => dest.Pets, opt => opt.MapFrom(src => src.Pets));
        
        CreateMap<CanilDTO, Canil>()
            .ForPath(dest => dest.User.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Descricao, opt => opt.MapFrom(src => src.Descricao))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CanilID));


        CreateMap<CanilRegisterDTO, Canil>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserID));

        CreateMap<Pet, PetRetornoDTO>()
            .ForMember(dest => dest.CanilId, opt => opt.MapFrom(src => src.CanilId)) // Mapear diretamente
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Cor, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest=>dest.CanilNome, opt=>opt.MapFrom(src=>src.Canil.User.UserName));
        
        CreateMap<Pet, PetDTO>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Cor, opt => opt.MapFrom(src => src.Color))
            .ForMember(dest => dest.CanilId, opt => opt.MapFrom(src => src.CanilId));
        
        CreateMap<PetDTO, Pet>()
            .ForMember(dest => dest.DataRegistro, opt => opt.Ignore())// Ignora DataDeRegistro
            .ForMember(dest=>dest.Name,opt=>opt.MapFrom(src=>src.Nome))
            .ForMember(dest=>dest.Color,opt=>opt.MapFrom(src=>src.Cor))
            .ForMember(dest=>dest.CanilId,opt=>opt.MapFrom(src=>src.CanilId));

        CreateMap<ApplicationUser, UserDTO>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest=>dest.UserID,opt=>opt.MapFrom(src=>src.Id));
    }
}