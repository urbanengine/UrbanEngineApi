using AutoMapper;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Models.CheckIn;
using UrbanEngine.Core.Models.Events;
using UrbanEngine.Core.Models.Rooms;
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

            CreateMap<EventEntity, EventListItemDto>();
            CreateMap<EventEntity, EventDetailDto>()
                .ReverseMap();

            CreateMap<CheckInEntity, CheckInListItemDto>();
            CreateMap<CheckInEntity, CheckInDetailDto>()
                .ReverseMap();

            CreateMap<string, RegionType>()
                .ConvertUsing(s => RegionType.FromName(s, true));

            CreateMap<string, EventType>()
                .ConvertUsing(s => EventType.FromName(s, true));

            CreateMap<RoomEntity, RoomListItemDto>();
            CreateMap<RoomEntity, RoomDetailDto>()
                .ReverseMap();
        }
    }
}
