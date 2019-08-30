
using AutoMapper;
using app.Domain.DTOs;
using app.Domain.Entities;

namespace Application.AutoMapper
{
    public class DataMappingProfile : Profile
    {
        public DataMappingProfile()
        {
            CreateMap<Vinculo, VinculoDTO>().ReverseMap(); 
            
        }
    }
}