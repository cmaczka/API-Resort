using AutoMapper;
using Resort.Modelos;
using Resort.Modelos.Dto;

namespace Resort
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            // Aquí puedes configurar tus mapeos
            CreateMap<Villa, VillaDto>().ReverseMap();
            CreateMap<Villa, VillaCreateDto>().ReverseMap();
            CreateMap<Villa, VillaUpdateDto>().ReverseMap();    

            CreateMap<VillaUpdateDto, Villa>()
                       .ForMember(dest => dest.RowVersion, opt => opt.Ignore()); // ✅ NO tocar RowVersion

            CreateMap<Villa, VillaUpdateDto>()
                .ForMember(dest => dest.RowVersion,
                    opt => opt.MapFrom(src => Convert.ToBase64String(src.RowVersion)));

            CreateMap<NumeroVilla, NumeroVillaCreateDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaUdpateDto>().ReverseMap();
            CreateMap<NumeroVilla, NumeroVillaDto>().ReverseMap();


        }

    }
}
