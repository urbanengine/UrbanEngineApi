using AutoMapper;
using UrbanEngine.Core.Entities;
using UrbanEngine.Core.Enums;
using UrbanEngine.Core.Models.CheckIn;
using UrbanEngine.Core.Models.Events;
using UrbanEngine.Core.Models.Users;
using UrbanEngine.Core.Models.Venues;

namespace UrbanEngine.Web.Configuration
{
	/// <summary>
	/// This class maps entities to their respective DTO's and vice versa
	/// </summary>
    public class AutoMapperProfile : Profile
    {
		/// <summary>
		/// Constructor for the AutoMapperProfile class
		/// </summary>
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

			CreateMap<UserEntity, UserListItemDto>();
			CreateMap<UserEntity, UserDetailDto>()
				.ReverseMap();

			CreateMap<string, RegionType>()
                .ConvertUsing(s => RegionType.FromName(s, true));

            CreateMap<string, EventType>()
                .ConvertUsing(s => EventType.FromName(s, true));


        }
    }
}
