using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Models.NewFolder;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionRequestDTO,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDTO, Region>().ReverseMap();
            CreateMap<AddWalksRequestDTO, Walk>().ReverseMap();
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<UpdateWalksRequestDTO, Walk>().ReverseMap();
            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
        }        
    }
}
