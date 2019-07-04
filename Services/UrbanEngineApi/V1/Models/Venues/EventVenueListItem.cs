using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UrbanEngine.Core.Application.Entities.ScheduleAggregate;
using UrbanEngine.Core.Application.Venues;

namespace UrbanEngine.Services.UrbanEngineApi.V1.Models.Venues
{
    /// <summary>
    /// represents an item to show in a list for event venues
    /// </summary>
    public class EventVenueListItem : IEventVenueModel
    {
        public long VenueId { get; set; }

        public string Name { get; set; }

        public string Location { get; set; }

        public string Region { get; set; }

        [JsonIgnore]
        public Expression<Func<EventVenue, IEventVenueModel>> Projection
        {
            get
            {
                return x => new EventVenueListItem
                {
                    VenueId = x.Id,
                    Name = x.Name,
                    Location = FormatLocation(x.Address, x.Address2, x.City, x.State, x.PostalCode, x.Country),
                    Region = x.Region.Name
                };
            }
        }
        
        private string FormatLocation(string address, string address2, string city, string state, string zip, string country)
        {
            var values = new[]
            {
                address, address2, city, state, zip, country
            };

            return string.Join(" ", values.Where(p => !string.IsNullOrEmpty(p)));
        }
    }
}
