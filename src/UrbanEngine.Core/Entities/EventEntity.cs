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
        public DateTimeOffset? StartDate { get; private set; } // required
        public DateTimeOffset? EndDate { get; private set; }
        public EventType EventType { get; private set; }
        public string OrganizerId { get; private set; } // required
		public long? RoomId { get; private set; } // required

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

        public IList<CheckInEntity> CheckIns { get; set; }

		public RoomEntity Room { get; set; }

        #endregion

        #region Constructors

        protected EventEntity()
        {

        }

        public EventEntity(long id, string name, DateTimeOffset startDate)
        {
            Id = id;
            Name = name;
            StartDate = startDate;
        }

        public EventEntity(string name, string description, EventType eventType, DateTimeOffset? startDate, DateTimeOffset? endDate, string organizerId, long? roomId)
        {
            Name = name;
            Description = description;
            StartDate = startDate;
            EndDate = endDate;
            EventType = eventType;
            OrganizerId = organizerId;
			RoomId = roomId;
        }

		public EventEntity(long id, string name, string description, EventType eventType, DateTimeOffset? startDate, DateTimeOffset? endDate, string organizerId, long? roomId)
			: this(name, description, eventType, startDate, endDate, organizerId, roomId)
        {
			Id = id;
		}

        #endregion
    }
}
