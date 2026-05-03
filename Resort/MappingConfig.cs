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
        }

    }
}
