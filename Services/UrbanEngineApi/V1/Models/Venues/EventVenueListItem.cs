using Newtonsoft.Json;
using System;
using System.Linq.Expressions;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Venues;

namespace UrbanEngine.Services.UrbanEngineApi.V1.Models.Venues
{
    /// <summary>
    /// represents an item to show in a list for event venues
    /// </summary>
    public class EventVenueListItem : IEventVenueModel
    {
        public string Name { get; set; }

        [JsonIgnore]
        public Expression<Func<EventVenue, IEventVenueModel>> Projection
        {
            get
            {
                return x => new EventVenueListItem
                {
                    Name = x.Name
                };
            }
        }
        
    }
}
