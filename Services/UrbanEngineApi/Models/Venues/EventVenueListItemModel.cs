using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Venues;

namespace UrbanEngine.Services.UrbanEngineApi.Models.Venues
{
    /// <summary>
    /// represents basic information to show for an event venue when displayed in a list
    /// not all details are displayed here see <see cref="EventVenueDetailModel"/>
    /// </summary>
    public class EventVenueListItemModel : IEventVenueModel
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        
        [JsonIgnore]
        public Expression<Func<EventVenue, IEventVenueModel>> Projection => x => FromDomainEntity(x);

        public EventVenue ToDomainEntity(long? id = null)
        {
            return new EventVenue(Id, Name);
        }
         
        public IEventVenueModel FromDomainEntity(EventVenue eventVenue)
        {
            return new EventVenueListItemModel
            {
                Id = eventVenue.Id,
                Name = eventVenue.Name
            };
        }
    }
}
