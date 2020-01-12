using System;
using System.Collections.Generic;
using UrbanEngine.Core.Enums;
using UrbanEngine.SharedKernel.Data;

namespace UrbanEngine.Core.Entities
{
    public class EventEntity : EntityBase
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

        public EventVenueEntity Venue { get; private set; }

        public IList<CheckInEntity> CheckIns { get; set; }

        #endregion

        #region Constructors

        protected EventEntity()
        {

        }

        public EventEntity(long id, string name, DateTime startDate)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
        }

        public EventEntity(string name, string description, EventType eventType, DateTime? startDate, DateTime? endDate, string organizerId, long? venueId)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            EventType = eventType;
            OrganizerId = organizerId;
            VenueId = venueId;
        }

        #endregion
    }
}
