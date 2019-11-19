using System;
using UrbanEngine.Core.Application.SharedKernel;

namespace UrbanEngine.Core.Application.Entities.ScheduleAggregate
{
    public class Event : Entity<long>
    {
        #region Properties 

        public string Name { get; private set; } // required
        public string Description { get; private set; } // required
        public DateTime? StartDate { get; private set; } // required
        public DateTime? EndDate { get; private set; }
        public EventType EventType { get; private set; }
        public string OrganizerId { get; private set; } // required
        public long? VenueId { get; private set; } // required

        public TimeSpan? Duration
        {
            get
            {
                if (StartDate.HasValue && EndDate.HasValue)
                {
                    return EndDate - StartDate;
                }

                return null;
            }
        }

        #endregion

        #region Navigation Properties

        public EventVenue Venue { get; private set; }

        #endregion

        #region Constructors

        protected Event()
        {

        }

        public Event(long id, string name, DateTime startDate)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
        }

        public Event(string name, string description, EventType eventType, DateTime? startDate, DateTime? endDate, string organizerId, long? venueId)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            EventType = eventType;
            OrganizerId = organizerId;
            VenueId = venueId;
        }

        /*
            id
            name
            description
            startdate (with time)
            duration ( end date - start date)
            EventVenue.Name
        */

        #endregion
    }
}
