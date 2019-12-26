using AutoMapper;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Models.Venues;

namespace UrbanEngine.Web.Configuration
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<EventVenueEntity, EventVenueListItemDto>();

            CreateMap<EventVenueEntity, EventVenueDetailDto>()
                .ReverseMap();
        }
    }
}
