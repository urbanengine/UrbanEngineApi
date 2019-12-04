using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Events;

namespace UrbanEngine.Services.UrbanEngineApi.Models.Events
{
    public class EventListItemModel : IEventModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string StartDate { get; set; }
        public string Duration { get; set; }
        public string VenueName { get; set; }

        [JsonIgnore]
        public Expression<Func<Event, IEventModel>> Projection => x => FromDomainEntity(x);

        public Event ToDomainEntity(long? id = null)
        {
            DateTime.TryParse(StartDate, out var startDate);
            
            return new Event(
                Id,
                Name,
                startDate);
        }

        public IEventModel FromDomainEntity(Event eventData)
        {
            return new EventListItemModel
            {
                Id = eventData.Id,
                Name = eventData.Name,
                Description = eventData.Description,
                StartDate = eventData.StartDate.HasValue ? eventData.StartDate.Value.ToString("g") : string.Empty,
                Duration = eventData.Duration.HasValue ? eventData.Duration.Value.ToString() : string.Empty,
                VenueName = eventData.Venue?.Name
            };
        }
    }
}
