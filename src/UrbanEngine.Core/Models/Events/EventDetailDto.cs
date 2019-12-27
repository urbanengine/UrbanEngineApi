using System;

namespace UrbanEngine.Core.Models.Events
{
    public class EventDetailDto
    {
        public  long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Type { get; set; }
        public string OrganizerId { get; private set; } = "1"; // TODO: need to remove hard coded value
        public long? VenueId { get; set; }
    }
}
